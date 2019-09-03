#!/bin/bash

androidapp=$PWD/../KennigDemoFinal/build/android
project_path=$PWD
echo $androidapp
echo "Build android application and deploy to AppCenter ..."

/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode  -quit -projectPath /Users/serapion/Documents/jenkins/workspace/workspace/KENNiG/KennigDemoFinal -executeMethod SerapionBuild.AndroidApp -logfile androidbuild.log

if [ -d $androidapp ]
then
cp -R $PWD/fastlane /"$androidapp"
cp Gemfile /"$androidapp"
cp Gemfile.lock /"$androidapp"
cd "$androidapp"
fastlane android upload_appcenter_android_stage
else
    echo "There is no new android version for build"
fi

echo "Building and deploying android app finished!"


