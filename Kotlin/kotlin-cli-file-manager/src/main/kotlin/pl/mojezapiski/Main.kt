package pl.mojezapiski

import kotlin.system.exitProcess

fun main() {
    val fm = MyFileManager()

    while (true){
        showMenu()
        performFileOperation(fm = fm, opt = getUserInput())
    }

}

fun performFileOperation(fm: MyFileManager, opt: Int) {
    when (opt) {
        1 -> performOpenFile(fm)
        2 -> performWriteFile(fm)
        3 -> performDeleteFile(fm)
        4 -> exitProcess(0)
    }

}

fun performDeleteFile(fm: MyFileManager) {
    fm.delete(
        path = setFilePath(),
        onFailure = { println(it) },
        onCompleted = { isSuccess ->
            if (isSuccess) println("Poprawnie usunieto plik")
            else println("Cos poszlo nie tak")
        }
    )
}

fun performWriteFile(fm: MyFileManager) {
    println("Wprowadz tekst >>")
    val text = readLine()!!.toString()
    fm.write(path = setFilePath(), text = text)
}

fun performOpenFile(fm: MyFileManager) {
    fm.open(
        path = setFilePath(),
        onFailure = { println(it) },
        onCompleted = { lines -> fm.printFile(lines) }
    )
}

fun getUserInput(): Int {
    print("Wybierz opcje >> ")
    while (true) {
        try {
            val input = readLine()!!.toInt()
            if (input !in 1..4) throw IllegalArgumentException("Liczba spoza zakresu")
            return input
        } catch (e: Exception) {
            println(e)
        }
    }
}

fun showMenu() {
    println("Menu")
    println("1. Otworz plik")
    println("2. Zapis do pliku")
    println("3. Usun plik")
    println("4. Wyjdz")
}

fun setFilePath(): String {
    print("Wskaz sciezke do pliku: ")
    return readLine()!!.toString()
}
