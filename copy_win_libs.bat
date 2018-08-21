if exist _bin_debug\ (
echo Copying Native Libs to debug
xcopy /A /R /E Libs\msvc\DLLx86\*.dll _bin_debug )

if exist _bin_debug\ (
echo Copying OpenTK to debug
xcopy /A /R /E Libs\OpenTK\Lib\*.* _bin_debug )


if exist _bin_release\ (
echo Copying to release
xcopy /A /R /E Libs\msvc\DLLx86\*.dll _bin_release )

if exist _bin_release\ (
echo Copying OpenTK to release
xcopy /A /R /E Libs\OpenTK\Lib\*.* _bin_release )