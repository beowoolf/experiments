#!/usr/bin/php -q
<?php

function libxml_display_error($error) {
    $return = "<br/>\n";
    switch ($error->level) {
        case LIBXML_ERR_WARNING:
            $return .= "<b>Warning $error->code</b>: ";
            break;
        case LIBXML_ERR_ERROR:
            $return .= "<b>Error $error->code</b>: ";
            break;
        case LIBXML_ERR_FATAL:
            $return .= "<b>Fatal Error $error->code</b>: ";
            break;
    }
    $return .= trim($error->message);
    if ($error->file) {
        $return .=    " in <b>$error->file</b>";
    }
    $return .= " on line <b>$error->line</b>\n";

    return $return;
}

function libxml_display_errors() {
    $errors = libxml_get_errors();
    foreach ($errors as $error) {
        print libxml_display_error($error);
    }
    libxml_clear_errors();
}

for ($i = 1; $i < 3; $i++)
	if (!isset($argv[$i])) {
		echo("Nie podano nazwy $i pliku.\n");
		exit($i);
	}

$typy_plikow = array();
$pliki = array($argv[1],$argv[2]);
$typy_plikow_do_sprawdzenia = array("xml","xsd");
foreach ($pliki as $plik)
	foreach ($typy_plikow_do_sprawdzenia as $typ_pliku_do_sprawdzenia)
		if (preg_match("/$typ_pliku_do_sprawdzenia$/", $plik) === 1) {
			$typy_plikow[$typ_pliku_do_sprawdzenia] = $plik;
			//echo("$plik jest plikiem $typ_pliku_do_sprawdzenia\n");
		}

$i = 3;
foreach ($typy_plikow_do_sprawdzenia as $typ_pliku_do_sprawdzenia)
	if (!isset($typy_plikow[$typ_pliku_do_sprawdzenia])) {
		echo("Brak pliku z rozszerzeniem $typ_pliku_do_sprawdzenia.\n");
		exit($i);
	}
	else
		$i++;

foreach ($typy_plikow_do_sprawdzenia as $typ_pliku_do_sprawdzenia)
	if (!is_file($typy_plikow[$typ_pliku_do_sprawdzenia])) {
		echo("Brak pliku {$typy_plikow[$typ_pliku_do_sprawdzenia]}.\n");
		exit($i);
	}
	else
		$i++;

// Enable user error handling
libxml_use_internal_errors(true);

$xml = new DOMDocument(); 
$xml->load($typy_plikow["xml"]);

if (!$xml->schemaValidate($typy_plikow["xsd"])) {
    echo("DOMDocument::schemaValidate() Generated Errors!\n");
    libxml_display_errors();
}
else
	echo("Zgodny\n");
