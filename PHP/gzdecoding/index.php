<?php

header("Content-Type: application/xml; charset=UTF-8");

$curl = curl_init();

curl_setopt_array($curl, [
  CURLOPT_URL => "https://www.pracuj.pl/SiteMaps/CurrentOffers/SiteMapJobOffers1.xml.gz",
  CURLOPT_RETURNTRANSFER => true,
  CURLOPT_ENCODING => "",
  CURLOPT_MAXREDIRS => 10,
  CURLOPT_TIMEOUT => 30,
  CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
  CURLOPT_CUSTOMREQUEST => "GET",
  CURLOPT_POSTFIELDS => "",
  CURLOPT_HTTPHEADER => [
    "User-Agent: Insomnia/2023.5.7"
  ],
]);

$response = curl_exec($curl);
$err = curl_error($curl);

curl_close($curl);

if ($err) {
  echo "cURL Error #:" . $err;
} else {
  echo gzdecode($response);
}
