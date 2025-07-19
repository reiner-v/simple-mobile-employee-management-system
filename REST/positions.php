<?php

include_once('connect.php');

	$position = mysqli_query($con,"SELECT sposition FROM salary");
    $allposition = array();


  if (mysqli_num_rows($position)==0)
    {
        echo "no rows";
    }
    else if (mysqli_num_rows($position)!=0)
    {
        while($value = mysqli_fetch_assoc($position))
        {  
            $allposition[] = $value;
        }
        
        echo json_encode($allposition);
    }
	mysqli_close($con);
?>