if exist _bin_debug\ (
echo Copying to debug
xcopy /A /R /E Libs\msvc\x64\*.dll _bin_debug )

if exist _bin_release\ (
echo Copying to release
xcopy /A /R /E Libs\msvc\x64\*.dll _bin_release )