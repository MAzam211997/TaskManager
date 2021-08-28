using System.ComponentModel;

namespace TaskManager.Entities.GeneralModels
{
    public enum AuditAppScreenTypes
    {
        Standard = 1, // crud, navigation
        Grid = 2,  // crud, navigation but with grid
        UpdateOnly = 3  // update only
    }

    public enum CommonOperations
    {
        //the 00 is for PageAccess and other customer operations should start from 10

        [Description("01")]
        AddNew = 18,

        [Description("02")]
        Update = 19,

        [Description("03")]
        Delete = 20,

        [Description("04")]
        Print = 622,

        [Description("05")]
        Email = 671
    }

    public enum OperationTypes
    {
        AccessPage = 1,
        TopToolBar = 2,
        IconsInsideGrid = 3
    }

    public enum AuditAppPageType
    {
        Page = 1,
        Report = 2,
        Setup = 3,
        None = 0
    }

    public enum AuditAppReportViewerType
    {
        Standard = 1,
        Expandable = 2,
        None = 10
    }

    public enum ReportType
    {
        Grid = 1,
        Receipt = 2
    }

    public enum DateTypes
    {
        FiscalYearStartDate = 1,
        FiscalYearEndDate = 2,
        FiscalYearPostedUptoDate = 3,
        PreviousYearBalanceDate = 4
    }

    public enum DateValidationTypes
    {
        NoCheck = 0,
        FormatCheck = 1,
        StartYearCheck = 2,
        EndYearCheck = 3,
        FullCheck = 4
    }

    public enum SalesInvoicePostingStatus
    {
        SuccessfullyPosted = 0,
        OutOfPeriod = 1, // 3655
        InvalidPostingSetup = 2, // 2427
        Others = 3, // 1304
        InvalidPostingSetupCostCenter = 5, // 2427 //584
        OutOfStock = 6 // 3772
    }

    public enum BatchTransactionType
    {
        StockBatchInward = 1,
        StockBatchOutward = 2
    }

    public enum KnownCommonReferences
    {
        Vendor,
        Buyer,
        SalesAgent,
        CollectionAgent,
        ProductDiscountSetup,
        SalesReturn
    }

    #region Pages

    // Page number is 7 digits in total, the seventh one will appear after the number of modules exceeds 9 
    // There will 10 menus in each module maximum.
    // 202005  = 0202005  [02|02|005] => 2nd module,  2nd menu and 5th  page
    // 405008  = 0405005  [04|05|005] => 4th module,  5th menu and 8th  page
    // 1209023 = 1209023  [12|09|023] => 12th module, 9th Menu and 23rd Page

    //First Module
    public enum CommonPages
    {
        [Description("40")]
        RolesPage = 101001,

        [Description("39")]
        UsersPage = 101002,

        [Description("41")]
        AccessRights = 101003,

        [Description("990")]
        UserProfile = 101004,

        [Description("2")]
        CompanyProfile = 101005,

        [Description("718")]
        ApplicationErrorsPage = 101006,

        [Description("718")]
        UserTemplates = 101007,

        [Description("1097")]
        Bugs = 101008,

        [Description("1101")]
        Activities = 101009,

        [Description("1106")]
        CountriesPage = 101010,

        [Description("1115")]
        StringsPage = 101011,

        [Description("977")]
        PasswordPage = 101012,

        [Description("37")]
        CurrenciesPage = 101013,

        [Description("559")]
        CurrencyRatesPage = 101014,

        [Description("3482")]
        NotificationSetupPage = 101015,

        [Description("3664")]
        FiscalPeriodPage = 101016,   // Fiscal Period Setup & Updates - Opening, Closing, Balance Updates etc.

        [Description("3863")]
        PrintSetupPage = 101017,

        [Description("4110")]
        TaxSetupPage = 101018,

        [Description("4035")]
        TaxRateMasterPage = 101019,

        [Description("4092")]
        Utilities_InventoryCostingPage = 101020,

        [Description("4103")]
        Utilities_RemoveDataPage = 101021,

        [Description("4456")]
        Utilities_TruncateHistoryPage = 101022,

        [Description("2010")]
        CardsPage = 101023,

        [Description("4464")]
        BanksPage = 101024,

        [Description("4465")]
        CardRateSetupPage = 101025,

        [Description("4521")]
        Utilities_RemoveBatchSerialsPage = 101026,
    };

    //2nd Module
    public enum FAPages
    {
        //Accounts (Master)

        [Description("34")]
        AccountMasterPage = 201001,

        [Description("1143")]
        OpeningBalancesPage = 201003,

        [Description("948")]
        PreviousYearBalancesPage = 206009,

        [Description("3634")]
        PLBSGroupNamesPage = 201004,

        ////Cost Centers

        [Description("558")]
        CostCentersPage = 201002,

        //3829
        [Description("1143")]
        CostCenterOpeningBalancesPage = 201005,

        //3830
        [Description("948")]
        CostCenterPreviousYearBalancesPage = 201006,

        [Description("4406")]
        CostCenterBudgetsPage = 201007,

        [Description("4416")]
        CostCenterAccountBudgetsPage = 201008,

        //Trasactions

        [Description("573")]
        ReceiptsPage = 202001,

        [Description("589")]
        PaymentsPage = 202002,

        [Description("592")]
        JournalVoucherPage = 202003,

        [Description("697")]
        InvoicesPage = 202004,

        [Description("1050")]
        VoucherParent = 202091,

        [Description("723")]
        CashReceiptPage = 202005,

        [Description("722")]
        CashPaymentPage = 202006,

        [Description("721")]
        BankReceiptPage = 202007,

        [Description("719")]
        BankPaymentPage = 202008,

        [Description("724")]
        DebitNotesPage = 202009,

        [Description("735")]
        CreditNotesPage = 202010,

        [Description("788")]
        PDCParent = 202092,

        [Description("1037")]
        PDCEntryPage = 202011,

        [Description("789")]
        PDC_Deposit = 202012,

        [Description("937")]
        PDC_Bounce = 202013,

        [Description("790")]
        PDC_Return = 202014,

        [Description("793")]
        PPCParent = 202093,

        [Description("1037")]
        PPCEntryPage = 202015,

        [Description("791")]
        PPC_Clear = 202016,

        [Description("792")]
        PPC_Return = 202017,

        [Description("1133")]
        TransactionViewPage = 202018,

        [Description("3929")]
        InvoiceCollectionPage = 202019,


        //Reports
        //Reports: Section-1

        [Description("3842")]
        ReportListingsParent = 203100,

        [Description("886")]
        ReportChartOfAccounts = 203001,

        [Description("991")]
        ReportCashBook = 203002,

        [Description("992")]
        ReportBankBook = 203003,

        [Description("666")]
        ReportJournalRegister = 203004,

        [Description("993")]
        ReportTransactionsListing = 203005,

        [Description("994")]
        ReportInvoiceListing = 203006,

        [Description("1440")]
        ReportInvoicePendingList = 203018,


        //Reports: Section-2

        [Description("3841")]
        ReportStatementsParent = 203200,

        [Description("1016")]
        ReportGeneralLedger = 203007,

        [Description("995")]
        ReportAccountsLedger = 203008,

        [Description("1017")]
        ReportTrialBalance = 203009,

        [Description("998")]
        ReportProfitLossStatement = 203010,

        [Description("999")]
        ReportBalanceSheet = 203011,

        [Description("1036")]
        ReportStatementOfAccounts = 203012,

        [Description("3821")]
        ReportAccountsAnalysis = 203013,

        [Description("3866")]
        ReportCashFlowStatement = 203014,

        [Description("3885")]
        ReportSubAccountLedger = 203015,

        [Description("3886")]
        ReportSubAccountTrial = 203016,

        [Description("3907")]
        ReportAccountBalanceAgeing = 203017,

        [Description("4206")]
        ReportAccountsTrial = 203019,



        //Cost Center Reports

        [Description("558")]
        ReportCostCenterParent = 203300,

        [Description("3825")]
        ReportCostCenterLedger = 203301,

        [Description("3857")]
        ReportCostCenterSummary = 203302,

        [Description("3860")]
        ReportCostCenterAnalysis = 203303,

        [Description("4407")]
        ReportCostCenterBudgetComparison = 203304,

        [Description("4408")]
        ReportCostCenterBudgetMonthlyAnalysis = 203305,

        [Description("4421")]
        ReportCostCenterAccountBudgetComparison = 203306,

        [Description("4424")]
        ReportCostCenterAccountBudgetMonthlyAnalysis = 203307,

        //Budgeting

        [Description("943")]
        BudgetsPage = 204001,

        [Description("634")]
        ProjectBudgetingPage = 204002,

        [Description("944")]
        BudgetsComparisonPage = 204003,

        [Description("945")]
        BudgetsExpenditurePage = 204004,

        [Description("1065")]
        CheckTransactionsPage = 205001,

        //Setup

        [Description("1115")]
        StringsPage = 206001,

        [Description("35")]
        AccountCodeSetupPage = 206002,

        [Description("36")]
        AccountBookSetupPage = 206003,

        [Description("946")]
        PLClassesSetupPage = 206004,

        [Description("947")]
        BSClassesSetupPage = 206005,

        [Description("641")]
        InvoicePostingSetupPage = 206006,

        [Description("752")]
        AccountVariableSetupPage = 206007,

        [Description("1606")]
        FA_StartingNumbersSetupPage = 206008,

        [Description("3482")]
        NotificationSetupPage = 206011,

        [Description("1322")]
        FAReportSetup = 206012,

        [Description("3863")]
        PrintSetupPage = 206013,

        //Global Setups
        [Description("1302")]
        GlobalSetupPage = 206014,

        [Description("4559")]
        CostCenterCodeSetupPage = 206015,

        [Description("4560")]
        AccruedVouchersPage = 206016,

        //Tools

        [Description("1092")]
        FormPrintFormatter = 207001,

        [Description("1090")]
        ThemesDesigner = 207002,
    };

    //3rd Module
    public enum FixedAssetsPages
    {
        //Master
        [Description("44")]
        AssetsPage = 301001,

        [Description("824")]
        LocationsPage = 301002,

        [Description("830")]
        AssetTransactionsPage = 301003,

        [Description("839")]
        AssetMaintenancesPage = 301004,

        [Description("863")]
        MaintenanceExpensesPage = 301005,

        [Description("1001")]
        DepreciationCalculationPage = 301006,

        [Description("1000")]
        ReportDepreciationStatementPage = 301007,

        [Description("3286")]
        ReportAssetsListPage = 301008,

        [Description("3295")]
        ReportAssetsLedgerPage = 301009,

        [Description("3296")]
        ReportTransactionsListingPage = 301010,

        [Description("1606")]
        FX_StartingNumbersSetupPage = 301011,

        [Description("1322")]
        FixedAssetsReportSetup = 304001,
    };

    //4th Module
    public enum InventoryPages
    {
        ///////////////// :: Masters :: ///////////////////
        //Unit
        [Description("1100")]
        UnitsPage = 401001,

        //Sales areas
        [Description("1102")]
        SalesAreasPage = 401002,

        //Warehouse
        [Description("866")]
        WarehousesPage = 401003,

        //Suppliers
        [Description("1108")]
        SuppliersPage = 401004,

        //Inventory Code Setup
        [Description("1112")]
        InventoryCodeSetupPage = 401005,

        //Item Card Master
        [Description("1113")]
        ItemCardMasterPage = 401006,

        //Salesman
        [Description("1187")]
        SalesmanPage = 401007,

        //Customers
        [Description("631")]
        CustomersPage = 401008,

        //Sales Collectors
        [Description("1411")]
        SalesCollectorsPage = 401009,

        //Opening stock
        [Description("1201")]
        OpeningStockPage = 401010,

        //Sales Groups
        [Description("1925")]
        SalesGroupPage = 401011,

        //Revise Selling Price
        [Description("4018")]
        ReviseSellingPricePage = 401012,

        //Stock Level
        [Description("4023")]
        InventoryStockLevelPage = 401013,

        //Opening stock File
        [Description("4298")]
        OpeningStockFilePage = 401014,

        //Stock Location
        [Description("4310")]
        StockLocationPage = 401015,

        //Item Inward Outward Tax Group
        [Description("4321")]
        InwardOutwardTaxGroupPage = 401016,

        //Warehouse Code Setup Page
        [Description("4558")]
        WarehouseCodeSetupPage = 401017,

        ///////////////// :: Purchase :: //////////////////

        //Purchase Quotation
        [Description("1279")]
        PurchaseQuotationPage = 402001,

        //Purchase Order
        [Description("1263")]
        PurchaseOrderPage = 402002,

        //Purchase Invoice
        [Description("894")]
        PurchaseInvoicePage = 402003,

        //Purchase Returns
        [Description("1268")]
        PurchaseReturnPage = 402004,

        //Payment on Credit Purchase
        [Description("1269")]
        PurchasePaymentPage = 402005,

        //Purchase Invoice Adjustment
        [Description("4308")]
        PurchaseInvoiceAdjustmentPage = 402006,

        //Goods Received Receipt
        [Description("1719")]
        GoodsReceivedReceiptPage = 402007,

        //Multiple Purchase Return
        [Description("4453")]
        MultiplePurchaseReturnPage = 402008,

        ///////////////// :: Sales :: //////////////////

        //Sales Quotation
        [Description("1287")]
        SalesQuotationPage = 403001,

        //Sales Order
        [Description("1288")]
        SalesOrderPage = 403002,

        //Sales Invoice
        [Description("893")]
        SalesInvoicePage = 403003,

        //Sales Return
        [Description("1224")]
        SalesReturnPage = 403004,

        //Sales Collection
        [Description("1289")]
        SalesCollectionPage = 403005,

        //Delivery Note
        [Description("3994")]
        GoodsDeliveryNotesPage = 403006,

        //Multiple Sales Return
        [Description("4454")]
        MultipleSalesReturnPage = 403007,

        ///////////////// :: Point of Sales :: ////////////////////////

        // POS - Sales Terminal
        [Description("2393")]
        POS_SalesTerminal = 404001,

        // POS - Setups
        [Description("2396")]
        POS_SetupsParent = 404091,

        // POS - Terminal Setup
        [Description("2397")]
        POS_TerminalSetup = 404002,

        // POS - Clerk/Screen Setup
        [Description("2398")]
        POS_ClerkScreenSetup = 404003,

        // POS - Shortcut Code Setup
        [Description("2399")]
        POS_ShortcutCodeSetup = 404004,

        // POS - Weigh-Scale Barcode Setup
        [Description("2400")]
        POS_WeighScaleBarcodeSetup = 404005,

        // POS - Reports
        [Description("664")]
        POS_ReportsParent = 404092,

        // POS - Transaction Listing
        [Description("2394")]
        POS_TransactionListing = 404006,

        // POS - Cash Drawer History
        [Description("2395")]
        POS_CashDrawerHistory = 404007,

        ///////////////// :: Others :: ////////////////////////

        // Stock Transfer
        [Description("1305")]
        StockTransferPage = 405001,

        // Stock Adjustment
        [Description("1313")]
        StockAdjustmentPage = 405002,

        // Stock Internal Use
        [Description("1321")]
        StockInternalUsePage = 405003,

        // Damaged Stock
        [Description("1320")]
        StockDamagePage = 405004,

        // Stock Adjustment Data Entry
        [Description("4180")]
        StockAdjustmentDataEntryPage = 405005,

        // Stock Transfer Request Page
        [Description("4214")]
        StockTransferRequestPage = 405006,

        //Zero Stock value Adjustment
        [Description("4458")]
        ZeroStockValueAdjustmentPage = 405007,

        //Remove Expired Stock Page
        [Description("4507")]
        RemoveExpiredStockPage = 405008,
        ///////////////// :: Stock Batch :: ////////////////////////

        // Stock Batch Inward
        [Description("4090")]
        StockBatchInwardPage = 401101,

        // Stock Batch Outward
        [Description("4136")]
        StockBatchOutwardPage = 401102,

        ///////////////// :: REPORTS :: ////////////////////////
        ////////////////////////////////////////////////////////


        /////// REPORTS : Inventory
        [Description("950")]
        InventoryReportsParent = 406100,

        [Description("2347")]
        Report_InventoryStockInUnits = 406101,

        [Description("1232")]
        Report_InventoryCurrentStock = 406102,

        [Description("1848")]
        Report_InventoryStockLedger = 406103,

        [Description("1862")]
        Report_InventoryCostCalculationChart = 406104,

        [Description("1908")]
        Report_InventoryItemFlowChart = 406105,

        //Keep this report as last one in the menu
        [Description("1233")]
        Report_InventoryOpeningStock = 406106,

        [Description("4003")]
        Report_InventoryTransactionSummary = 406107,

        [Description("4017")]
        Report_InventoryPriceList = 406108,

        [Description("4026")]
        Report_InventoryStockMaintenance = 406109,

        [Description("4252")]
        Report_InventorySupplierListing = 406110,

        [Description("4253")]
        Report_InventoryCustomerListing = 406111,

        [Description("4254")]
        Report_InventorySalesmanListing = 406112,

        [Description("4323")]
        Report_InventoryItemInwardOutwardTaxListing = 406113,

        [Description("4324")]
        Report_InventoryItemLocationListing = 406114,

        [Description("4325")]
        Report_InventoryItemIDSListing = 406115,

        [Description("4405")]
        Report_InventoryStockOnDate = 406116,


        /////// REPORTS : Purchase
        [Description("1098")]
        PurchaseReportsParent = 406200,

        [Description("1326")]
        Report_PurchaseInvoiceListing = 406201,

        [Description("1342")]
        Report_PurchaseInvoiceItemListing = 406202,

        [Description("1384")]
        Report_PurchaseOrderListing = 406203,

        [Description("1406")]
        Report_PurchaseOrderItemListing = 406204,

        [Description("1408")]
        Report_PurchaseOrderPendingItemListing = 406205,

        [Description("1440")]
        Report_PurchaseInvoicePendingList = 406206,

        [Description("1412")]
        Report_PurchasePaymentListing = 406207,

        [Description("1458")]
        Report_PurchaseReturnListing = 406208,

        [Description("4368")]
        Report_PurchaseQuotationListing = 406209,

        [Description("4369")]
        Report_PurchaseQuotationItemListing = 406210,

        [Description("4370")]
        Report_PurchaseQuotationPendingItemListing = 406211,

        [Description("4400")]
        Report_GRRListing = 406212,

        [Description("4401")]
        Report_GRRItemListing = 406213,

        [Description("4402")]
        Report_GRRPendingItemListing = 406214,

        /////// REPORTS : Sales
        [Description("1099")]
        SalesReportsParent = 406300,

        [Description("3319")]
        Report_SalesInvoiceSummary = 406309,

        [Description("1376")]
        Report_SalesInvoiceListing = 406301,

        [Description("1377")]
        Report_SalesInvoiceItemListing = 406302,

        [Description("1409")]
        Report_SalesOrderListing = 406303,

        [Description("1410")]
        Report_SalesOrderItemListing = 406304,

        [Description("1433")]
        Report_SalesOrderPendingItemListing = 406305,

        [Description("1440")]
        Report_SalesInvoicePendingList = 406306,

        [Description("1419")]
        Report_SalesCollectionListing = 406307,

        [Description("1459")]
        Report_SalesReturnListing = 406308,

        [Description("3903")]
        Report_SalesMonthlyAnalysis = 406310,

        [Description("4275")]
        Report_CustomerExcessCreditList = 406311,

        [Description("4371")]
        Report_SalesQuotationListing = 406312,

        [Description("4372")]
        Report_SalesQuotationItemListing = 406313,

        [Description("4373")]
        Report_SalesQuotationPendingItemListing = 406314,

        [Description("4413")]
        Report_GoodsDeliveryNoteListing = 406315,

        [Description("4414")]
        Report_GoodsDeliveryNoteItemListing = 406316,

        [Description("4415")]
        Report_GoodsDeliveryNonInvoicedListing = 406317,

        [Description("4443")]
        Report_SalesmanCommission = 406318,

        [Description("4449")]
        Report_SalesMovingNonMovingItems = 406319,

        [Description("4512")]
        Report_SalesInvoiceAgeing = 406320,


        /////// REPORTS : Others
        [Description("1304")]
        OthersReportsParent = 406400,

        [Description("1495")]
        Report_StockTransferListing = 406401,

        [Description("1496")]
        Report_StockInternalUseListing = 406402,

        [Description("1590")]
        Report_StockDamageListing = 406403,


        /////// REPORTS : Production
        [Description("3188")]
        ProductionReportsParent = 406500,

        [Description("4154")]
        Report_ProductionProcessListing = 406501,


        /////// REPORTS : Batches
        [Description("4181")]
        BatchesReportsParent = 406600,

        [Description("4182")]
        Report_BatchesItemBatch = 406601,

        [Description("4183")]
        Report_BatchesItemExpiry = 406602,

        [Description("4259")]
        Report_SerialsItemBatch = 406603,

        [Description("4506")]
        Report_BatchExpired = 406604,
        ///////////////////////////////////////////////////////


        /////// REPORTS : VAT / Tax Report
        [Description("4198")]
        VatTaxReportsParent = 406700,

        [Description("4199")]
        Report_VatTaxClaim = 406701,

        [Description("4296")]
        Report_TaxClaimItems = 406702,

        [Description("4384")]
        Report_TaxAssessment = 406703,


        ///////////////// :: Setups :: ////////////////////////
        [Description("1115")]
        StringsPage = 407001,

        //Global Setups
        [Description("1302")]
        GlobalSetupPage = 407002,

        //Inventory Posting Setup
        [Description("1306")]
        InventoryPostingSetupPage = 407003,

        //Purchase Posting Setup
        [Description("1225")]
        PurchasePostingSetupPage = 407004,

        //Sales Posting Setup
        [Description("1226")]
        SalesPostingSetupPage = 407005,

        [Description("1606")]
        INV_StartingNumbersSetupPage = 407006,

        //Customize Inventory Reports
        [Description("1322")]
        InventoryReportSetup = 407007,

        [Description("3482")]
        NotificationSetupPage = 407008,

        [Description("3863")]
        PrintSetupPage = 407009,

        // Sales Credit Limit / Discount Control
        //[Description("4215")]
        // Sales Credit Limit Permission
        [Description("4274")]
        SalesCreditControlPage = 407010,

        //Production :  Master
        [Description("557")]
        ProductionMasterParent = 408000,

        [Description("3194")]
        ProductionMasterPage = 408001,

        [Description("3192")]
        ProductionFinishedGoodsPage = 408002,

        [Description("3193")]
        ProductionRawMaterialsPage = 408003,

        [Description("3300")]
        ProductionSupportingItemsPage = 408004,

        [Description("3182")]
        ProductionExpensesPage = 408005,

        [Description("3195")]
        ProductionWastesPage = 408006,

        [Description("3299")]
        ProductionCostingPage = 408007,

        //Production :  Process
        [Description("1165")]
        ProductionProcessParent = 408100,

        [Description("3317")]
        ProductionProcessPage = 408101,

        [Description("4504")]
        ProductionAccruedSetupPage = 408102,


        ///////////////////////////////////////////////////////

        ///////////////// :: Barcode :: ////////////////////////
        [Description("3788")]
        BarcodeLabelSetupPage = 409001,

        //Global Setups
        [Description("3789")]
        BarcodeLabelPrintingPage = 409002,

        ///////////////////////////////////////////////////////
    }

    //6th Module
    public enum ManufacturingPages
    {
        //Master
        [Description("557")]
        MastersPage = 601001,

        [Description("588")]
        TransactionsPage = 601002,

        [Description("664")]
        ReportsPage = 601003,

        [Description("2391")]
        SetupsPage = 601004,

    };

    //7th Module
    public enum POSPages
    {
        ///////////////// :: Masters :: ///////////////////

        ///////////////// :: Transactions :: ///////////////////

        ///////////////// :: Reports :: ///////////////////
        [Description("3660")]
        PostedTransactions = 703001,

        [Description("3661")]
        UnpostedTransactions = 703002,

        [Description("2997")]
        HoldedTransactions = 703003,

        ///////////////// :: Setups :: ///////////////////
        [Description("2440")]
        CounterPage = 704001,

        [Description("2441")]
        DevicePage = 704002,

        [Description("2442")]
        UserPage = 704003,

        [Description("2962")]
        ItemGroupsPage = 704004,

        [Description("3096")]
        ReceiptSetupPage = 704005,

        [Description("4251")]
        WeighScaleSetupPage = 704006,

        [Description("4582")]
        PrinterGroupsPage = 704007,
    };

    //8th Module
    public enum HRPages
    {
        ///////////////// :: Employee :: ///////////////////
        //Personal Information
        [Description("3201")]
        EmployeesPage = 501001,

        //Packages
        [Description("3309")]
        EmployeePackagesPage = 501002,

        //Provisions
        [Description("3138")]
        EmployeeProvisionsPage = 501003,

        //Documents
        [Description("1460")]
        EmployeeDocumentsPage = 501004,

        [Description("1946")]
        EmployeeVacationPage = 501005,


        ///////////////// :: Masters :: ///////////////////
        //Branches
        [Description("2319")]
        BranchesPage = 502001,

        //Departments
        [Description("3122")]
        DepartmentsPage = 502002,

        //Designations
        [Description("3124")]
        DesignationsPage = 502003,

        //Document Types
        [Description("3117")]
        DocumentTypesPage = 502004,

        //Employee Types
        [Description("3279")]
        EmployeeTypesPage = 502005,

        //Provision Categories
        [Description("3275")]
        ProvisionCategoriesPage = 502006,

        //Qualifications
        [Description("3221")]
        QualificationsPage = 502007,

        //Religions
        [Description("3226")]
        ReligionsPage = 502008,


        ///////////////// :: Transactions :: ///////////////////
        //Daily Absents
        [Description("3136")]
        EmployeeAbsentDailyPage = 503001,

        //Monthly Absents
        [Description("3127")]
        EmployeeAbsentMonthlyPage = 503002,

        //Daily Lates
        [Description("3338")]
        EmployeeLatesDailyPage = 503003,

        //Monthly Lates
        [Description("3312")]
        EmployeeLatesMonthlyPage = 503004,

        //Daily Overtime
        [Description("3313")]
        EmployeeOvertimeDailyPage = 503005,

        //Monthly Overtime
        [Description("3314")]
        EmployeeOvertimeMonthlyPage = 503006,

        //Loans
        [Description("1965")]
        EmployeeLoansPage = 503007,

        //Salary Advance
        [Description("3311")]
        EmployeeSalaryAdvancePage = 503008,

        //Branch Transfers
        [Description("3103")]
        EmployeeBranchTransfersPage = 503009,

        //Vacation Calculator
        [Description("1949")]
        EmployeeVacationCalculatorPage = 503010,

        //Non Monthly Allowances And DeductionsPage
        [Description("3423")]
        NonMonthlyAllowancesAndDeductionsPage = 503011,


        ///////////////// :: Statements :: ///////////////////

        //Salary Statement
        [Description("890")]
        SalaryStatement = 504001,

        //Salary Detail Statement
        [Description("3535")]
        SalaryDetailStatement = 504002,

        //Pay Slip
        [Description("2237")]
        PaySlipStatement = 504003,

        //Allowances Statement
        [Description("891")]
        AllowancesStatement = 504004,

        //Deductions Statement
        [Description("2179")]
        DeductionsStatement = 504005,

        //Provisions Statement
        [Description("2234")]
        ProvisionsStatement = 504006,

        //Absents Statement
        [Description("2948")]
        AbsentsStatement = 504007,

        //Lates Statement
        [Description("3536")]
        LatesStatement = 504008,

        //Overtimes Statement
        [Description("3542")]
        OvertimesStatement = 504009,

        //Loans Statement
        [Description("1965")]
        LoanStatement = 504010,

        //Loan Detail Statement
        [Description("2207")]
        LoanDetailStatement = 504011,

        //Salary Advance Statement
        [Description("3145")]
        SalaryAdvanceStatement = 504012,

        //PaySlip Detail
        [Description("2238")]
        PaySlipDetail = 504013,

        ///////////////// :: Report :: ///////////////////

        //Report Absents
        [Description("2948")]
        ReportAbsents = 507001,

        //Report Lates
        [Description("3536")]
        ReportLates = 507002,

        //Report Overtime
        [Description("3542")]
        ReportOvertimes = 507003,

        //Report Loans
        [Description("1965")]
        ReportLoans = 507004,

        //Report Salary Advance
        [Description("3145")]
        ReportSalaryAdvance = 507005,

        //Report Branch Transfer
        [Description("3103")]
        ReportBranchTransfer = 507006,

        //Report Vacation Calculator
        [Description("1949")]
        ReportVacationCalculator = 507007,

        //Report Expiry Documents
        [Description("3537")]
        ReportExpiryDocuments = 507008,

        //Report EmployeeListing
        [Description("4311")]
        ReportEmployeeListing = 507009,

        ///////////////// :: Setup :: ///////////////////
        [Description("1115")]
        StringsPage = 508001,

        [Description("3310")]
        EmployeeProvisionFormulasPage = 508002,

        [Description("3496")]
        AbsentTypesPage = 508003,

        [Description("3419")]
        LateTypesPage = 508004,

        [Description("3263")]
        OvertimeTypesPage = 508005,

        [Description("3119")]
        DocumentUploadPage = 508006,

        [Description("1606")]
        HR_StartingNumbersSetupPage = 508007,

        [Description("3482")]
        NotificationSetupPage = 508008,

        //Global Setups
        [Description("1302")]
        GlobalSetupPage = 508009,

        [Description("3556")]
        SalaryPostingSetupPage = 508010,

        //Customize Inventory Reports
        [Description("1322")]
        HRReportSetup = 508011
    };

    #endregion
}
