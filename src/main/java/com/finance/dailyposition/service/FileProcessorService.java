package com.finance.dailyposition.service;

import com.finance.dailyposition.service.csv.FileProcessor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.util.Map;

@Service
public class FileProcessorService {

    private final Map<String, FileProcessor> processors;

    @Autowired
    public FileProcessorService(Map<String, FileProcessor> processors) {
        this.processors = processors;
    }

    public void process(String financial, MultipartFile file) {
        FileProcessor processor = processors.get(financial);
        if (processor == null) {
            throw new IllegalArgumentException("Financial processor not found: " + financial);
        }
        processor.processFile(file);
    }
}
