#!/bin/sh


iosapp=$PWD/../KennigDemoFinal/build/iPhone
project_path=$PWD
echo $project_path
echo $iosapp
echo "Build IOS application and deploy to AppCenter and TestFlight ..."

/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode  -quit -projectPath /Users/serapion/Documents/jenkins/workspace/workspace/KENNiG/KennigDemoFinal -executeMethod SerapionBuild.IphoneApp -logFile iosbuild.log

if [ -d "$iosapp" ]
then
cp -R $PWD/fastlane /"$iosapp"
cp Gemfile /"$iosapp"
cp Gemfile.lock /"$iosapp"
cd "$iosapp"
ls
security unlock-keychain -p a ${HOME}/Library/Keychains/login.keychain
security set-keychain-settings -t 3600 -l ${HOME}/Library/Keychains/login.keychain
fastlane ios build_stage
fastlane ios upload_appcenter_stage
fastlane ios build_preproduction
fastlane ios upload_testflight
else
echo "There is no new IOS version for build!"
fi

echo "Building and deploying IOS App finished!"
