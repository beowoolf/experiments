package pl.mojezapiski.cryptomarket

import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication

@SpringBootApplication
class CryptoMarketApplication

fun main(args: Array<String>) {
	runApplication<CryptoMarketApplication>(*args)
}
