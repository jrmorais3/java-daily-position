package com.finance.dailyposition.rest;


import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.ss.usermodel.*;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Iterator;

@RestController
@RequestMapping("/api/read")
public class ReadExcelFileController {

    public static final String ITAU = "ITAÚ";
    public static final String SANTANDER = "SANTANDER";

    @PostMapping
    public ResponseEntity<?> readExcelFile(@RequestParam("file") MultipartFile file) throws IOException {
        processExcelFile(file, "14/01/2025");
        return ResponseEntity.ok().build();
    }

    public void processExcelFile(MultipartFile file, String data) throws IOException {
        try (InputStream inputStream = file.getInputStream()) {
            Workbook workbook = new HSSFWorkbook(inputStream);

            final Sheet sheet = workbook.getSheetAt(0);
            String sheetName = sheet.getSheetName();

            // Checking if the sheet is ITAÚ or SANTANDER
            if (sheetName.startsWith(ITAU)) {
                processItauSheet(sheet, data);
            } else if (sheetName.startsWith(SANTANDER)) {
                processSantanderSheet(sheet, data);
            }

            // Write changes back to the file
            try (FileOutputStream fos = new FileOutputStream("novo_arquivo.xls")) {
                workbook.write(fos);
            }
        }
    }

    private void processItauSheet(Sheet sheet, String data) {
        String conta = sheet.getSheetName().substring(5, 12);

        // Deleting rows 1 to 6 and specific columns
        deleteRows(sheet, 0, 6);  // Delete rows 1 to 6 (index 0 to 5)
        deleteColumns(sheet, new int[]{0, 2, 3}); // Delete columns A, C, D (index 0, 2, 3)

        // Removing content in column C
        clearColumn(sheet, 2);  // C column is index 2

        // Auto resize columns (For simplicity, this is just resizing based on content)
        autoResizeColumns(sheet);

        // Handle deleting rows based on the condition
        deleteRowsBasedOnData(sheet, data);

        // Formatting the date in column A
        formatDateColumn(sheet, 0);  // A column is index 0
    }

    private void processSantanderSheet(Sheet sheet, String data) {
        String conta = sheet.getSheetName().substring(10, 21);

        // Deleting rows 1 to 3 and specific columns
        deleteRows(sheet, 0, 3);  // Delete rows 1 to 3 (index 0 to 2)
        deleteColumns(sheet, new int[]{1, 5}); // Delete columns B and F (index 1, 5)

        // Removing content in column C
        clearColumn(sheet, 2);  // C column is index 2

        // Handle deleting rows based on the condition
        deleteRowsBasedOnData(sheet, data);
    }

    private void deleteRows(Sheet sheet, int startRow, int endRow) {
        for (int i = endRow - 1; i >= startRow; i--) {
            Row row = sheet.getRow(i);
            if (row != null) {
                sheet.removeRow(row);
            }
        }
    }

    private void deleteColumns(Sheet sheet, int[] columnsToDelete) {
        for (int colIndex : columnsToDelete) {
            for (int i = 0; i < sheet.getPhysicalNumberOfRows(); i++) {
                Row row = sheet.getRow(i);
                if (row != null) {
                    Cell cell = row.getCell(colIndex);
                    if (cell != null) {
                        row.removeCell(cell);
                    }
                }
            }
        }
    }

    private void clearColumn(Sheet sheet, int columnIndex) {
        for (Row row : sheet) {
            Cell cell = row.getCell(columnIndex);
            if (cell != null) {
                cell.setCellValue("");
            }
        }
    }

    private void autoResizeColumns(Sheet sheet) {
        Row row = sheet.getRow(0);
        if(row != null) {
            int physicalNumberOfCells = row.getPhysicalNumberOfCells();
            for (int i = 0; i < physicalNumberOfCells; i++) {
                sheet.autoSizeColumn(i);
            }
        }
    }

    private void deleteRowsBasedOnData(Sheet sheet, String data) {
        Iterator<Row> rowIterator = sheet.iterator();
        while (rowIterator.hasNext()) {
            Row row = rowIterator.next();
            Cell cell = row.getCell(0); // Assuming the data is in the first column
            if (cell != null && !cell.getStringCellValue().equals(data)) {
                rowIterator.remove();
            }
        }
    }

    private void formatDateColumn(Sheet sheet, int columnIndex) {
        DataFormat format = sheet.getWorkbook().createDataFormat();
        for (Row row : sheet) {
            Cell cell = row.getCell(columnIndex);
            if (cell != null && cell.getCellType() == CellType.STRING) {
                try {
                    // Assuming the format is in dd/mm/yyyy
                    cell.setCellValue(cell.getStringCellValue());
                    cell.getCellStyle().setDataFormat(format.getFormat("dd/mm/yyyy"));
                } catch (Exception e) {
                    // Handle potential format errors
                }
            }
        }
    }
}

