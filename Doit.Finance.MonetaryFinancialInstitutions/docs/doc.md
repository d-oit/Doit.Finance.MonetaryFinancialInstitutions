<a name='assembly'></a>
# Doit.Finance.MonetaryFinancialInstitution

## Contents

- [DownloadExtension](#T-Doit-Finance-MonetaryFinancialInstitutions-Extensions-DownloadExtension 'Doit.Finance.MonetaryFinancialInstitutions.Extensions.DownloadExtension')
  - [DownloadFileAsync(url,fileInfo)](#M-Doit-Finance-MonetaryFinancialInstitutions-Extensions-DownloadExtension-DownloadFileAsync-System-String,System-IO-FileInfo- 'Doit.Finance.MonetaryFinancialInstitutions.Extensions.DownloadExtension.DownloadFileAsync(System.String,System.IO.FileInfo)')
- [GermanyBankSortCodeHelper](#T-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper 'Doit.Finance.MonetaryFinancialInstitutions.GermanyBankSortCodeHelper')
  - [DownloadFileAsync(url)](#M-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper-DownloadFileAsync-System-String- 'Doit.Finance.MonetaryFinancialInstitutions.GermanyBankSortCodeHelper.DownloadFileAsync(System.String)')
  - [GetCurrentBankSortList(download)](#M-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper-GetCurrentBankSortList-System-Boolean- 'Doit.Finance.MonetaryFinancialInstitutions.GermanyBankSortCodeHelper.GetCurrentBankSortList(System.Boolean)')
  - [GetListFromCsv()](#M-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper-GetListFromCsv 'Doit.Finance.MonetaryFinancialInstitutions.GermanyBankSortCodeHelper.GetListFromCsv')
- [MonetaryFinancialInstitution](#T-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution')
  - [DownloadCurrentCsvAsync()](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-DownloadCurrentCsvAsync-System-String- 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.DownloadCurrentCsvAsync(System.String)')
  - [GetCsvList(fileName)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetCsvList-System-String- 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetCsvList(System.String)')
  - [GetCurentListAsync(downloadCsv)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetCurentListAsync-System-Boolean- 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetCurentListAsync(System.Boolean)')
  - [GetCurentMrrListAsync(downloadCsv)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetCurentMrrListAsync-System-Boolean- 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetCurentMrrListAsync(System.Boolean)')
  - [GetMfiList(fileName)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiList 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiList')
  - [GetMfiMrrUpdateCsv(fileName)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiMrrUpdateCsv-System-String- 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiMrrUpdateCsv(System.String)')
  - [GetMfiUpdateCsv(fileName)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiUpdateCsv-System-String- 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiUpdateCsv(System.String)')
  - [GetMfiUpdateList(fileName)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiUpdateList 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiUpdateList')
  - [GetMrrCsvList(fileName)](#M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMrrCsvList-System-String- 'Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMrrCsvList(System.String)')

<a name='T-Doit-Finance-MonetaryFinancialInstitutions-Extensions-DownloadExtension'></a>
## DownloadExtension `type`

##### Namespace

Doit.Finance.MonetaryFinancialInstitutions.Extensions

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-Extensions-DownloadExtension-DownloadFileAsync-System-String,System-IO-FileInfo-'></a>
### DownloadFileAsync(url,fileInfo) `method`

##### Summary

Downloads a file from a given URL and saves it to the specified file path.

##### Returns

A Task that represents the asynchronous operation.
The task result is the full path of the downloaded file if successful, otherwise null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL from which to download the file. |
| fileInfo | [System.IO.FileInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.FileInfo 'System.IO.FileInfo') | The FileInfo object representing the destination file path. |

<a name='T-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper'></a>
## GermanyBankSortCodeHelper `type`

##### Namespace

Doit.Finance.MonetaryFinancialInstitutions

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper-DownloadFileAsync-System-String-'></a>
### DownloadFileAsync(url) `method`

##### Summary

Downloads the latest CSV file from the Bundesbank website.

##### Returns

The name of the downloaded CSV file.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL of the Bundesbank website containing the download link. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Thrown when the website has changed and the href containing the download file name is not found. |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper-GetCurrentBankSortList-System-Boolean-'></a>
### GetCurrentBankSortList(download) `method`

##### Summary

Retrieves the current list of German bank sort codes.

##### Returns

A list of BankAccount objects containing the current German bank sort codes.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| download | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | A flag indicating whether to download the latest CSV file from the Bundesbank website. Default is true. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ApplicationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ApplicationException 'System.ApplicationException') | Thrown when bad records are found in the CSV file. |
| [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Thrown when the website has changed and the href containing the download file name is not found. |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-GermanyBankSortCodeHelper-GetListFromCsv'></a>
### GetListFromCsv() `method`

##### Summary

Retrieves a list of BankAccount objects from the CSV file.

##### Returns

A list of BankAccount objects.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ApplicationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ApplicationException 'System.ApplicationException') | Thrown when bad records are found in the CSV file. |

<a name='T-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution'></a>
## MonetaryFinancialInstitution `type`

##### Namespace

Doit.Finance.MonetaryFinancialInstitutions

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-DownloadCurrentCsvAsync-System-String-'></a>
### DownloadCurrentCsvAsync() `method`

##### Summary

Downloads the current CSV asynchronous.

##### Returns



##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Website has changed. Check for Full Database and Update table row description! |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetCsvList-System-String-'></a>
### GetCsvList(fileName) `method`

##### Summary

Gets the Monetary Financial Institutions CSV as IEnumerable.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the file. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ApplicationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ApplicationException 'System.ApplicationException') | Check csv file for bad records! |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetCurentListAsync-System-Boolean-'></a>
### GetCurentListAsync(downloadCsv) `method`

##### Summary

Gets the curent Monetary Financial Institutions list asynchronous.
Without BIC

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| downloadCsv | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` [download CSV]. |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetCurentMrrListAsync-System-Boolean-'></a>
### GetCurentMrrListAsync(downloadCsv) `method`

##### Summary

Gets the curent Monetary Financial Institutions (MFIs) subject to the Eurosystem's minimum reserve requirement (monthly data) asynchronous.
(With BIC)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| downloadCsv | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | if set to `true` [download CSV]. |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiList'></a>
### GetMfiList(fileName) `method`

##### Summary

Gets the list.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [M:Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiList](#T-M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiList 'M:Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiList') | Name of the file. |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiMrrUpdateCsv-System-String-'></a>
### GetMfiMrrUpdateCsv(fileName) `method`

##### Summary

Gets the Monetary Financial Institutions (MFIs) subject to the Eurosystem's minimum reserve requirement (monthly data) update CSV.

##### Returns

Returns a list of MfiMinimunReserveRequirementUpdate objects.
Throws an ApplicationException if there are any bad records in the CSV file.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the file. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ApplicationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ApplicationException 'System.ApplicationException') | Check csv file for bad records! |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiUpdateCsv-System-String-'></a>
### GetMfiUpdateCsv(fileName) `method`

##### Summary

Gets the Monetary Financial Institutions update CSV.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the file. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ApplicationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ApplicationException 'System.ApplicationException') | Check csv file for bad records! |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiUpdateList'></a>
### GetMfiUpdateList(fileName) `method`

##### Summary

Gets the update list.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [M:Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiUpdateList](#T-M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMfiUpdateList 'M:Doit.Finance.MonetaryFinancialInstitutions.MonetaryFinancialInstitution.GetMfiUpdateList') | Name of the file. |

<a name='M-Doit-Finance-MonetaryFinancialInstitutions-MonetaryFinancialInstitution-GetMrrCsvList-System-String-'></a>
### GetMrrCsvList(fileName) `method`

##### Summary

Gets the Monetary Financial Institutions (MFIs) subject to the Eurosystem's minimum reserve requirement (monthly data) CSV as IEnumerable.

##### Returns

Returns a list of MfiMinimunReserveRequirement objects.
Throws an ApplicationException if there are any bad records in the CSV file.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the file. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ApplicationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ApplicationException 'System.ApplicationException') | Check csv file for bad records! |
