<?php

$connection = mysqli_connect("localhost", "root", "root", "unityaccess");

$sql = "SELECT * FROM players ORDER BY id DESC";
$result = mysqli_query($connection, $sql); 

if ($result){
    while ($row = mysqli_fetch_assoc($result))
    {
    echo $row["username"] . "," . $row["salt"] . "*"; 
    }
}
else {
    echo "error";
}

?>

