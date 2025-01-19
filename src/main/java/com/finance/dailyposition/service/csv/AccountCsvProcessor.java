package com.finance.dailyposition.service.csv;

import com.finance.dailyposition.jpa.entities.Account;
import com.finance.dailyposition.utils.CsvUtils;
import com.finance.dailyposition.utils.DateUtils;
import com.opencsv.CSVReader;
import org.springframework.stereotype.Component;

import java.io.StringReader;
import java.util.Arrays;
import java.util.List;
import java.util.Optional;

@Component
public class AccountCsvProcessor {

    public static final String AGENCIA_CONTA = "Agência/Conta:";
    public static final String NOME = "Nome:";
    public static final String EXTRATO_DE = "Extrato de";
    public static final String DATA = "Data:";

    public Account processCsv(String csvContent) {
        try (CSVReader csvReader = new CSVReader(new StringReader(csvContent))) {
            List<String[]> rows = csvReader.readAll();
            List<String[]> filteredRows = rows.stream()
                    .map(CsvUtils::removeEmptyColumns)
                    .filter(row -> row.length > 0)
                    .toList();
            String agencyAndAccount = extractAgencyAndAccount(filteredRows);

            Account account = new Account();
            account.setAgency(agencyAndAccount.split("/")[0]);
            account.setNumber(agencyAndAccount.split("/")[1]);
            account.setBank("ITAU");
            account.setAccountDate(DateUtils.parseDate(extractDate(filteredRows)));

            return account;

        } catch (Exception e) {
            throw new RuntimeException("Error processing CSV", e);
        }
    }

    private String extractAgencyAndAccount(List<String[]> rows) {
        Optional<String[]> firstNonEmptyRow = rows.stream()
                .filter(row -> Arrays.stream(row).anyMatch(col -> col != null && !col.trim().isEmpty()))
                .findFirst();

        if (firstNonEmptyRow.isPresent()) {
            String[] row = firstNonEmptyRow.get();
            return Arrays.stream(row)
                    .filter(col -> col != null && col.contains(AGENCIA_CONTA))
                    .findFirst()
                    .map(col -> col.replace(AGENCIA_CONTA, "").trim())
                    .orElseThrow(() -> new RuntimeException("Agency and Account not found in the first non-empty row"));
        }

        throw new RuntimeException("No non-empty rows found in the file");
    }

    private String extractBank(List<String[]> rows) {
        return rows.stream()
                .flatMap(Arrays::stream)
                .filter(col -> col != null && col.contains(NOME))
                .map(col -> col.replace(NOME, "").trim())
                .findFirst()
                .orElseThrow(() -> new RuntimeException("Name not found in CSV"));
    }

    private String extractDate(List<String[]> rows) {
        return rows.stream()
                .flatMap(Arrays::stream)
                .filter(col -> col != null && col.contains(DATA))
                .map(col -> {
                    System.out.println("Before cleaning: [" + col + "]");
                    String cleaned = col.replace(DATA, "").replace("\u00A0", "").trim();
                    System.out.println("After cleaning: [" + cleaned + "]");
                    return cleaned;
                })
                .findFirst()
                .orElseThrow(() -> new RuntimeException("Date not found in CSV"));
    }

    private String extractAgency(List<String[]> rows) {
        return rows.stream()
                .flatMap(Arrays::stream)
                .filter(col -> col != null && col.contains(AGENCIA_CONTA))
                .map(col -> col.split("/")[0].replace(AGENCIA_CONTA, "").trim())
                .findFirst()
                .orElseThrow(() -> new RuntimeException("Agency not found in CSV"));
    }

    private String extractAccount(List<String[]> rows) {
        return rows.stream()
                .flatMap(Arrays::stream)
                .filter(col -> col != null && col.contains(AGENCIA_CONTA))
                .map(col -> col.split("/")[1].trim())
                .findFirst()
                .orElseThrow(() -> new RuntimeException("Account not found in CSV"));
    }

    private String extractExtractDate(List<String[]> rows) {
        return rows.stream()
                .flatMap(Arrays::stream)
                .filter(col -> col != null && col.contains(EXTRATO_DE))
                .map(col -> col.replace(EXTRATO_DE, "").trim())
                .findFirst()
                .orElse(null);
    }
}
