package com.finance.dailyposition.service;

import org.springframework.web.multipart.MultipartFile;

public interface FileProcessor {
    void processFile(MultipartFile file);
}
