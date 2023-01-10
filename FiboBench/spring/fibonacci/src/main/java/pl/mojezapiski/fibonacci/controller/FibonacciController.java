package pl.mojezapiski.fibonacci.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/spring")
public class FibonacciController {
    @GetMapping
    public int fibonaci(int i) {
        if (i <= 1) return i;
        else return fibonaci(i - 2) + fibonaci(i - 1);
    }
}
