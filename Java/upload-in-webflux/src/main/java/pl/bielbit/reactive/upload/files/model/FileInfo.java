package pl.bielbit.reactive.upload.files.model;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class FileInfo {
    private String name;
    private String url;
}
