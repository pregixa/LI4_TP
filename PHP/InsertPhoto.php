<?php
	require_once(dirname(__FILE__).'/ConnectionInfo.php');

	
if (isset($_POST['Foto']) && isset($_POST['IdVis']))
{
	//Get the POST variables
	$foto = $_POST['Foto'];
	$idVis = $_POST['IdVis'];
	
	//Set up our connection
	$connectionInfo = new ConnectionInfo();
	$connectionInfo->GetConnection();
	
	if (!$connectionInfo->conn)
	{
		//Connection failed
		echo 'No Connection';
	}
	
	else
	{
		$imgData = base64_decode($foto);
		
		//Insert foto
		$query = "INSERT INTO Foto(foto_file,visita) VALUES
					(CONVERT(VARBINARY(MAX), ?), ?)";
		//$parameters = array($foto, $idVis);
		$parameters = array($imgData, $idVis);

		//Execute query
		$stmt = sqlsrv_query($connectionInfo->conn, $query, $parameters);
		
		if (!$stmt)
		{	//The query failed
			echo 'Query Failed';	
		}
		
		else
		{
			//The query succeeded
			echo 'Successful';	
		}
	}
}

?>