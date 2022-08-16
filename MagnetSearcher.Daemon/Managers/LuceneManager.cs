using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Cn.Smart;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MagnetSearcher.Models;

namespace MagnerSearcher.Managers {
    public class LuceneManager {
        public LuceneManager() {
        }
        public async Task WriteDocumentAsync(MagnetInfo info) {
            using var writer = GetIndexWriter(0);
            try {
                Document doc = new Document();
                doc.Add(new TextField("InfoHash", info.InfoHash, Field.Store.YES));
                doc.Add(new TextField("Name", info.Name, Field.Store.YES));
                doc.Add(new TextField("Files", JsonConvert.SerializeObject(info.Files, Formatting.Indented), Field.Store.YES));

                writer.AddDocument(doc);
                writer.Flush(triggerMerge: true, applyAllDeletes: true);
                writer.Commit();
            } catch (ArgumentNullException ex) {
                Console.WriteLine(ex.Message);
            }
        }
        public void WriteDocuments(IEnumerable<MagnetInfo> infos) {
            using (var writer = GetIndexWriter(0)) {
                foreach ((MagnetInfo info, Document doc) in from info in infos
                                                            let doc = new Document()
                                                            select (info, doc)) {
                    try {
                        doc.Add(new TextField("InfoHash", info.InfoHash, Field.Store.YES));
                        doc.Add(new TextField("Name", info.Name, Field.Store.YES));
                        doc.Add(new TextField("Files", JsonConvert.SerializeObject(info.Files, Formatting.Indented), Field.Store.YES));
                        writer.AddDocument(doc);
                    } catch (ArgumentNullException ex) {
                        Console.WriteLine(ex.Message);
                    }

                }
                writer.Flush(triggerMerge: true, applyAllDeletes: true);
                writer.Commit();
            }

        }
        private IndexWriter GetIndexWriter(long GroupId) {
            var dir = FSDirectory.Open($"Data/Index_Data_{GroupId}");
            var analyzer = new SmartChineseAnalyzer(LuceneVersion.LUCENE_48);
            var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
            IndexWriter writer = new IndexWriter(dir, indexConfig);
            return writer;
        }

        private List<string> GetKeyWords(string q) {
            List<string> keyworkds = new List<string>();
            var analyzer = new SmartChineseAnalyzer(LuceneVersion.LUCENE_48);
            using (var ts = analyzer.GetTokenStream(null, q)) {
                ts.Reset();
                var ct = ts.GetAttribute<Lucene.Net.Analysis.TokenAttributes.ICharTermAttribute>();

                while (ts.IncrementToken()) {
                    StringBuilder keyword = new StringBuilder();
                    for (int i = 0; i < ct.Length; i++) {
                        keyword.Append(ct.Buffer[i]);
                    }
                    string item = keyword.ToString();
                    if (!keyworkds.Contains(item)) {
                        keyworkds.Add(item);
                    }
                }
            }
            return keyworkds;
        }
        public (int, List<MagnetInfo>) Search(string q, int Skip, int Take) {
            IndexReader reader = DirectoryReader.Open(FSDirectory.Open($"Data/Index_Data_0"));

            var searcher = new IndexSearcher(reader);

            var keyWordQuery = new BooleanQuery();
            foreach (var item in GetKeyWords(q)) {
                keyWordQuery.Add(new TermQuery(new Term("Name", item)), Occur.MUST);
            }
            var orQuery = new BooleanQuery();
            orQuery.Add(keyWordQuery, Occur.SHOULD);
            var top = searcher.Search(keyWordQuery, Skip + Take, new Sort(new SortField("MessageId", SortFieldType.INT64, true)));
            var total = top.TotalHits;
            var hits = top.ScoreDocs;

            var magnetInfos = new List<MagnetInfo>();
            var id = 0;
            foreach (var hit in hits) {
                if (id++ < Skip) continue;
                var document = searcher.Doc(hit.Doc);
                magnetInfos.Add(new MagnetInfo() {
                    InfoHash = document.Get("InfoHash"),
                    Name = document.Get("Name"),
                    Files = !string.IsNullOrEmpty(document.Get("Files")) ? JsonConvert.DeserializeObject<List<MagnetFile>>(document.Get("Files")) : new List<MagnetFile>()
                });
            }
            return (total, magnetInfos);
        }
    }
}
