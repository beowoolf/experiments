<?php

$u_ids = array();
$file = "videopoint";
$certificates = array();
$html_file = "$file.html";
// $html_code = file_get_contents($html_file);
$dom = new DomDocument();
// $dom->loadHTML($html_code);
$dom->loadHTMLFile($html_file);
$rows = $dom->getElementsByTagName("tr");
$certificate = array("c" => "", "d" => "", "i" => "");
foreach($rows as $id => $row)
    if($id > 0)
        foreach($row->childNodes as $i => $v)
            if($v->nodeName == "td")
                switch($i) {
                    case 1:
                    case 3:
                        $certificate[($i == 1 ? "c" : "d")] = $v->nodeValue;
                        break;
                    case 5:
                        //echo("<pre>");
                        foreach($v->childNodes[1]->attributes as $k => $val)
                            if($k=="href") $certificate["i"] = str_replace("/users/dyplom?id=", "", $val->nodeValue);
                        if($i==5 && !isset($u_ids[$certificate["i"]]))
                            $certificates[] = $certificate;
                        if($i==5)
                            $u_ids[$certificate["i"]] = true;
                        break;
                        //var_dump($certificate);
                        //echo("</pre>");
                        //echo("<hr/>");
                    /*case 7:
                        foreach($v->childNodes[0]->attributes as $k => $val)
                            if($k=="href") $certificate[($i == 5 ? "i" : "p")] = str_replace("dyplom.cgi?id=", "", $val->nodeValue);
                        if($i==7 && !isset($u_ids[$certificate["i"]]))
                            $certificates[] = $certificate;
                        if($i==7)
                            $u_ids[$certificate["i"]] = true;
                        break;*/
                }
foreach($certificates as $k => $v)
    unset($certificates[$k]["p"]);

$download_dir = __DIR__."/certificates";
$dir_to_downloaded_png_certs = "$download_dir/png";
$dir_to_downloaded_pdf_certs = "$download_dir/pdf";
if (!file_exists($download_dir))
    mkdir($download_dir);
foreach (array("pdf", "png") as $type) {
    $path = "$download_dir/$type";
    if (!file_exists($path))
        mkdir($path);
}

$final_certificates = array();
$bash_lines = array();
$bash_lines[] = "#!/bin/bash";
//'curl 'https://videopoint.pl/users/dyplom?id=\xb8c66c25f58de3bc9f14ece1edbd9a30f1e766ff08b25d417cebfb6c2b561863&action=publish&format=png'   -H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9'   -H 'Accept-Language: pl'   -H 'Connection: keep-alive'   -H 'Sec-Fetch-Dest: document'   -H 'Sec-Fetch-Mode: navigate'   -H 'Sec-Fetch-Site: none'   -H 'Sec-Fetch-User: ?1'   -H 'Upgrade-Insecure-Requests: 1'   -H 'User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.76'   -H 'sec-ch-ua: "Not?A_Brand";v="8", "Chromium";v="108", "Microsoft Edge";v="108"'   -H 'sec-ch-ua-mobile: ?0'   -H 'sec-ch-ua-platform: "Windows"'   --compressed > xb8c66c25f58de3bc9f14ece1edbd9a30f1e766ff08b25d417cebfb6c2b561863.png'
$download_prefix_base_url = "https://videopoint.pl/users/dyplom?id=";
foreach ($certificates as $key => $value)
    foreach (array("pdf", "png") as $type) {
        $cert_id_index = 'i';
        $cert_id = $value[$cert_id_index];
        // Initialize a file URL to the variable
        $url = $download_prefix_base_url . $cert_id . ($type === "png" ? "&action=publish&format=png" : "");
        
        // Initialize the cURL session
        $ch = curl_init($url);
        
        // Initialize directory name where
        // file will be save
        $dir = "$download_dir/$type/";
        
        // Use basename() function to return
        // the base name of file
        $file_name = "$cert_id.$type";
        
        // Save file into file location
        $save_file_loc = $dir . $file_name;
        
        // Open file
        $fp = fopen($save_file_loc, 'wb');
        
        // It set an option for a cURL transfer
        curl_setopt($ch, CURLOPT_FILE, $fp);
        curl_setopt($ch, CURLOPT_HEADER, 0);
        
        // Perform a cURL session
        curl_exec($ch);
        
        // Closes a cURL session and frees all resources
        curl_close($ch);
        
        // Close file
        fclose($fp);

        $new_value = $value;
        $new_value[$cert_id_index] = str_replace("\\", "", $cert_id);
        $final_certificates[$key] = $new_value;

        $bash_lines[] = "curl --silent '$url'   -H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9'   -H 'Accept-Language: pl'   -H 'Connection: keep-alive'   -H 'Sec-Fetch-Dest: document'   -H 'Sec-Fetch-Mode: navigate'   -H 'Sec-Fetch-Site: none'   -H 'Sec-Fetch-User: ?1'   -H 'Upgrade-Insecure-Requests: 1'   -H 'User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.76'   -H 'sec-ch-ua: \"Not?A_Brand\";v=\"8\", \"Chromium\";v=\"108\", \"Microsoft Edge\";v=\"108\"'   -H 'sec-ch-ua-mobile: ?0'   -H 'sec-ch-ua-platform: \"Windows\"'   --compressed > $type/".$new_value[$cert_id_index].".$type &";
    }
$current_timestamp = time();
foreach ($bash_lines as $key => $value)
    error_log("$value\n", 3, "$download_dir/download_script-$current_timestamp.bash");
$json_str = json_encode($final_certificates);
file_put_contents("$file.json", $json_str);
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
echo($json_str); // do ["i"] należy dokleić prefix https://videopoint.pl/users/dyplom.cgi?id=

?>
