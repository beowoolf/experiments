package pl.bielbit.reactive.upload.files.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.buffer.DataBuffer;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.http.codec.multipart.FilePart;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.util.UriComponentsBuilder;
import pl.bielbit.reactive.upload.files.model.FileInfo;
import pl.bielbit.reactive.upload.files.model.ResponseMessage;
import pl.bielbit.reactive.upload.files.service.FileStorageService;
import reactor.core.publisher.Flux;
import reactor.core.publisher.Mono;

import java.util.stream.Stream;

@RestController
@CrossOrigin("http://localhost:8081")
public class FileController {
    @Autowired
    FileStorageService storageService;

    @PostMapping("/upload")
    public Mono<ResponseEntity<ResponseMessage>> uploadFile(@RequestPart("file") Mono<FilePart> filePartMono) {
        return storageService.save(filePartMono).map(
                (filename) -> ResponseEntity.ok().body(new ResponseMessage("Uploaded the file successfully: " + filename)));
    }

    @GetMapping("/files")
    public ResponseEntity<Flux<FileInfo>> getListFiles() {
        Stream<FileInfo> fileInfoStream = storageService.loadAll().map(path -> {
            String filename = path.getFileName().toString();
            String url = UriComponentsBuilder.newInstance().path("/files/{filename}").buildAndExpand(filename).toUriString();
            return new FileInfo(filename, url);
        });

        Flux<FileInfo> fileInfosFlux = Flux.fromStream(fileInfoStream);

        return ResponseEntity.status(HttpStatus.OK).body(fileInfosFlux);
    }

    @GetMapping("/files/{filename:.+}")
    public ResponseEntity<Flux<DataBuffer>> getFile(@PathVariable String filename) {
        Flux<DataBuffer> file = storageService.load(filename);

        return ResponseEntity.ok().header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=\"" + filename + "\"")
                .contentType(MediaType.APPLICATION_OCTET_STREAM).body(file);
    }
}
