#!/bin/bash

set -e

basedir=$(cd $(dirname $0) ; pwd)/..
stack='dotnet-webhook-gh-database'
template='database.yaml'
s3bucket='4fun'
s3prefix='dotnet-webhook-gh/cloudformation'
profile='default'

rm -rf $basedir/dist && mkdir $basedir/dist

cp $basedir/aws/$template $basedir/dist

aws cloudformation package \
  --template-file $basedir/dist/$template \
  --output-template-file $basedir/dist/deploy.yaml \
  --s3-bucket $s3bucket \
  --s3-prefix $s3prefix \
  --profile $profile

aws cloudformation deploy \
  --stack-name $stack \
  --template-file $basedir/dist/deploy.yaml \
  --profile $profile
