# Do.Finance.MonetaryFinancialInstitution

Get the latest Monetary Financial Institutions (MFIs) from the https://www.ecb.europa.eu Website as IEnumerable 

**ECB User guide**
https://www.ecb.europa.eu/stats/financial_corporations/list_of_financial_institutions/html/mfi_userguide.en.html

**General download information website**
https://www.ecb.europa.eu/stats/financial_corporations/list_of_financial_institutions/html/elegass.en.html

## Requirement

Check the if the csv files are avaible at the url:

https://www.ecb.europa.eu/stats/financial_corporations/list_of_financial_institutions/html/daily_list-MID.en.html

**Website structure:**

### Monetary Financial Institutions (MFIs): Download area

The EU population of MFIs


|               | compressed                   | uncompressed              |
|---------------|------------------------------|---------------------------|
| Full Database | mfi_csv_201005.csv.gz        | mfi_csv_201005.csv        |
| Update        | mfi_csv_update_201005.csv.gz | mfi_csv_update_201005.csv |


https://www.ecb.europa.eu/stats/financial_corporations/list_of_financial_institutions/html/monthly_list-MID.en.html

### Monetary Financial Institutions (MFIs) subject to the Eurosystem's minimum reserve requirement (monthly data): Download area

The EU population of MFIs

|               | compressed                       | uncompressed                 |
|---------------|----------------------------------|------------------------------|
| Full Database | mfi_mrr_csv_200930.csv.gz        | mfi_mrr_csv_200930.csv       |
| Update        | mfi_mrr_csv_update_200930.csv.gz | mfi_mrr_csv_update_200930.csv|


## Usage

- Download the latest csv from the ecb website

   ```csharp
   var result = await monetaryFinancialInstitution.GetCurentListAsync(true);
  ```

- Use supplied csv files **mfi_csv_201001.csv** / **mfi_csv_update_201001.csv**
  ```csharp
   var result = await monetaryFinancialInstitution.GetCurentListAsync(false);
  ```
