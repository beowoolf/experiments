package pl.mojezapiski.fibonacci.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import reactor.core.publisher.Mono;

@RestController
@RequestMapping("/webflux")
public class FibonacciController {
    public int fibonaci(int i) {
        if (i <= 1) return i;
        else return fibonaci(i - 2) + fibonaci(i - 1);
    }
    @GetMapping
    public Mono<Integer> getFibonaci(int i) {
        return Mono.just(fibonaci(i));
    }
}
