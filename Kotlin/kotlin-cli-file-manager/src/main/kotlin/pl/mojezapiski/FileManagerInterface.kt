package pl.mojezapiski

interface FileManagerInterface {
    fun open(
        path: String,
        onFailure: (Exception) -> Unit,
        onCompleted: (List<String>) -> Unit
    )

    fun delete(
        path: String,
        onFailure: (Exception) -> Unit,
        onCompleted: (Boolean) -> Unit
    )

    fun write(path: String, text: String)

    fun printFile(lines: List<String>)
}
