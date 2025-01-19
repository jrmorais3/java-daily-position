package com.finance.dailyposition.service.csv;

import org.springframework.stereotype.Component;
import org.springframework.web.multipart.MultipartFile;

@Component("SANTANDER")
public class SantanderProcessor implements FileProcessor {

    @Override
    public void processFile(MultipartFile file) {
        System.out.println("OK SANTANDER");
    }
}
