$studio = 'C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\devenv.exe'
$solution = '.\DevShell.sln'
& $studio $solution /Rebuild Release

$source = ".\DevShells\bin\Release"
$app = "DevShells.exe"
$dest = 'D:\Tools\Scripts'

Copy-Item -Path "$source\$app" -Destination $dest -Force
Copy-Item -Path "$source\Newtonsoft.Json.dll" -Destination $dest -Force

if (!(Test-Path "$dest\$app.config"))
{
	Copy-Item -Path "$source\$app.config" -Destination $dest
}
