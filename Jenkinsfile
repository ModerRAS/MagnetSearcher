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
    stage('Build DotNet Project') {
      parallel {
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
      }
    }
  }
}