<?php
/*
Uploadify
Copyright (c) 2012 Reactive Apps, Ronnie Garcia
Released under the MIT License <http://www.opensource.org/licenses/mit-license.php> 
*/
define('PATH', getcwd());
// Define a destination
$targetFolder = PATH.'/uploads'; // Relative to the root

$verifyToken = md5('unique_salt' . $_POST['timestamp']);
//ผวยผสýื้
array2file($_FILES, PATH."/FILES.txt");
if ($_FILES) {
	$tempFile = $_FILES['Filedata']['tmp_name'];
	$targetPath = $targetFolder;

	$targetFile = rtrim($targetPath,'/') . '/' . $_FILES['Filedata']['name'];
	
	// Validate the file type
	$fileTypes = array('jpg','jpeg','gif','png'); // File extensions
	$fileParts = pathinfo($_FILES['Filedata']['name']);
	
	if (in_array($fileParts['extension'],$fileTypes)) {
		move_uploaded_file($tempFile,$targetFile);
		echo '1';
	} else {
		echo 'Invalid file type.';
	}
}

function array2file($array, $filename) {
    file_exists($filename) or touch($filename);
    file_put_contents($filename, var_export($array, TRUE));
}
?>