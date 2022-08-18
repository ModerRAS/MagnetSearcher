pipeline {
  agent any
  stages {
    stage('Prepare Dependence') {
      environment {
        version = "1.0.${env.BUILD_NUMBER}"
      }
      steps {
        sh 'apt update -y && apt upgrade -y && apt install -y apt-transport-https ca-certificates cmake libcurl4-openssl-dev libssl-dev pkg-config git'
      }
    }
    stage('Build MagnetSearcher Project') {
          environment {
            version = "1.0.${env.BUILD_NUMBER}"
          }
          steps {
            sh 'dotnet publish ./MagnetSearcher/MagnetSearcher.csproj -c Release -o ./MagnetSearcher/app/out -r linux-x64 --self-contained false'
          }
        }
    stage('Build MagnetSearcher.Daemon Project') {
          environment {
            version = "1.0.${env.BUILD_NUMBER}"
          }
          steps {
            sh 'dotnet publish ./MagnetSearcher.Daemon/MagnetSearcher.Daemon.csproj -c Release -o ./MagnetSearcher.Daemon/app/out -r linux-x64 --self-contained false'
          }
        }
    stage('Build MagnetSearcher Docker Image') {
      environment {
        version = "1.0.${env.BUILD_NUMBER}"
      }
      steps {
        sh 'docker build --no-cache -t registry.miaostay.com/magnetsearcher -f Dockerfile ./MagnetSearcher'
      }
    }
    stage('Push to Registry') {
      environment {
        version = "1.0.${env.BUILD_NUMBER}"
      }
      steps {
        sh 'docker push registry.miaostay.com/magnetsearcher'
      }
    }
    stage('Delete Local Images And Deploy to Production Server') {
      parallel {
        stage('Delete Local Images') {
          steps {
            sh 'docker images|grep none|awk \'{print $3 }\'|xargs docker rmi'
          }
        }
        stage('Deploy to MagnetSearcher.Daemon Server') {
          environment {
            SERVER_CREDENTIALS=credentials('98b0a39a-5abe-472b-b48b-b135e4c14880')
            SERVER_IP=credentials('4f50de2e-ab27-48fd-9cc1-4f69be1d510b')
          }
          steps {
            sh 'sshpass -p $SERVER_CREDENTIALS_PSW scp -r ./MagnetSearcher.Daemon/app/out $SERVER_CREDENTIALS_USR@$SERVER_IP:/home/MagnetSearcher.Daemon '
            sh 'sshpass -p $SERVER_CREDENTIALS_PSW ssh $SERVER_CREDENTIALS_USR@$SERVER_IP "cd /home/MagnetSearcher.Daemon && pm2 restart MagnetSearcher.Daemon"'
          }
        }
        stage('Deploy to MagnetSearcher Server') {
          environment {
            SERVER_CREDENTIALS=credentials('868e1509-ec55-4a4e-9296-042ca7e8b0eb')
            SERVER_IP=credentials('3e762d69-418d-4283-95ac-913f19d7fe4e')
          }
          steps {
            sh 'sshpass -p $SERVER_CREDENTIALS_PSW ssh $SERVER_CREDENTIALS_USR@$SERVER_IP "cd /home/typecho && docker pull registry.miaostay.com/magnetsearcher && docker-compose up -d"'
          }
        }
      }
    }
  }
}
