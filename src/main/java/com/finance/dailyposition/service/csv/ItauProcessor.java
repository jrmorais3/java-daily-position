package com.finance.dailyposition.service.csv;

import com.finance.dailyposition.jpa.entities.Account;
import com.finance.dailyposition.jpa.repositories.AccountRepository;
import org.springframework.stereotype.Component;
import org.springframework.web.multipart.MultipartFile;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;

@Component("ITAU")
public class ItauProcessor implements FileProcessor {

    private final AccountRepository accountRepository;
    private final AccountCsvProcessor accountCsvProcessor;

    public ItauProcessor(AccountRepository accountRepository, AccountCsvProcessor accountCsvProcessor) {
        this.accountRepository = accountRepository;
        this.accountCsvProcessor = accountCsvProcessor;
    }

    @Override
    public void processFile(MultipartFile file) {
        try (InputStream inputStream = file.getInputStream();
             InputStreamReader reader = new InputStreamReader(inputStream, StandardCharsets.ISO_8859_1)) {
            String csvContent = new BufferedReader(reader).lines().reduce("", (acc, line) -> acc + line + "\n");
            Account account = accountCsvProcessor.processCsv(csvContent);

            accountRepository.save(account);
            System.out.println("Account Processed: " + account);

        } catch (Exception e) {
            throw new RuntimeException("Error processing file", e);
        }
    }
}
