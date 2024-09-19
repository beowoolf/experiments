package pl.mojezapiski

import java.io.File
import java.io.FileOutputStream
import java.io.FileReader

class MyFileManager : FileManagerInterface {
    override fun open(
        path: String,
        onFailure: (Exception) -> Unit,
        onCompleted: (List<String>) -> Unit
    ) {

        var fileReader: FileReader? = null
        try {
            fileReader = FileReader(path)
            fileReader.readLines()?.let { onCompleted(it) }

        } catch (e: Exception) {
            onFailure(e)
        } finally {
            fileReader?.close()
        }

    }

    override fun delete(
        path: String,
        onFailure: (Exception) -> Unit,
        onCompleted: (Boolean) -> Unit
    ) {
        val file = File(path)
        try {
            val result = file.delete()
            onCompleted(result)
        } catch (e: Exception) {
            onFailure(e)
        }
    }

    override fun write(
        path: String,
        text: String
    ) {
        val file = File(path)
        FileOutputStream(file).use { out ->
            out.write(text.toByteArray())
        }

    }

    override fun printFile(lines: List<String>) {
        for (line in lines) {
            println(line)
        }
    }

}
