package pl.mojezapiski

import java.io.BufferedReader
import java.io.FileReader
import java.io.FileWriter

fun main(args: Array<String>) {

    val bufferedReader =
        BufferedReader(FileReader("D:\\clients.csv"))
    val fileWriter =
        FileWriter(
            "D:\\clients.csv",
            true
        )

    val csvManager = CsvManger(bufferedReader, fileWriter)

    val csvRecords = csvManager.readCsv()
    val clients = csvManager.getClients(csvRecords)

    for (client in clients) {
        println("Name: ${client.name}")
    }

    val newClient = Client("Jon", "Smith", "789")
    csvManager.writeCsv(listOf(newClient))

}
