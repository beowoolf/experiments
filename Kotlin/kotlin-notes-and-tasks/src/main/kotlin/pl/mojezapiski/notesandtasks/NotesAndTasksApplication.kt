package pl.mojezapiski.notesandtasks

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication

@SpringBootApplication
class NotesAndTasksApplication

fun main(args: Array<String>) {
	runApplication<NotesAndTasksApplication>(*args)
}
