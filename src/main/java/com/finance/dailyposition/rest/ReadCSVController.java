package com.finance.dailyposition.rest;

import com.finance.dailyposition.service.FileProcessorService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.text.Normalizer;

@RestController
@RequestMapping("/v1/api/import")
public class ReadCSVController {

    private final FileProcessorService fileProcessingService;

    @Autowired
    public ReadCSVController(FileProcessorService fileProcessingService) {
        this.fileProcessingService = fileProcessingService;
    }

    @PostMapping(path = "/csv")
    public ResponseEntity<?> readCvsFile(@RequestParam("file") MultipartFile file) throws IOException {
        try {
            String financial = extractFinancialInstitution(file);
            fileProcessingService.process(financial, file);
            return ResponseEntity.ok("Arquivo processado com sucesso para a financeira: " + financial);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.status(500).body("Erro ao processar arquivo: " + e.getMessage());
        }
    }

    public String extractFinancialInstitution(MultipartFile file) {
        String fileName = file.getOriginalFilename();

        if (fileName == null || fileName.trim().isEmpty()) {
            throw new IllegalArgumentException("File name cannot be null or empty");
        }

        String normalizedFileName = Normalizer.normalize(fileName, Normalizer.Form.NFD).replaceAll("\\p{M}", "");
        String[] parts = normalizedFileName.split(" ");

        if (parts.length == 0 || parts[0].trim().isEmpty()) {
            throw new IllegalArgumentException("Could not determine financial institution from file name: " + fileName);
        }

        return parts[0].toUpperCase();
    }

}
