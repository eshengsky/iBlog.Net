?param($installPath, $toolsPath, $package, $project)

$path = [System.IO.Path]
$readmefile = $path::Combine($path::GetDirectoryName($project.FileName), "confighelper.readme.txt")
$DTE.ItemOperations.OpenFile($readmefile)