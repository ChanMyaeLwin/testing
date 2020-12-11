using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DAL_AmigoProcess.BOL;

namespace DAL_AmigoProcess.DAL
{
    #region CUSTOMER MASTER
    public class CUSTOMER_MASTER
    {        
        #region ConnectionSetUp

        public string strConnectionString;
        string strMessage;
        string strInsert = @"Insert into CUSTOMER_MASTER(COMPANY_NO_BOX,TRANSACTION_TYPE,EFFECTIVE_DATE,UPDATE_CONTENT,COMPANY_NAME,COMPANY_NAME_READING,ESTIMATED_SUBMISSION_DATE,
                            CONTRACT_DATE,COMPLETION_NOTE_SENDING_DATE,CONTRACTOR_DEPARTMENT_IN_CHARGE,CONTRACTOR_CONTACT_NAME,CONTRACTOR_CONTACT_NAME_READING,CONTRACTOR_POSTAL_CODE,
                            CONTRACTOR_ADDRESS,[CONTRACTOR_ADDRESS-2],CONTRACTOR_MAIL_ADDRESS,CONTRACTOR_PHONE_NUMBER,BILL_SUPPLIER_NAME,BILL_SUPPLIER_NAME_READING,BILL_COMPANY_NAME,
                            BILL_DEPARTMENT_IN_CHARGE,BILL_CONTACT_NAME,BILL_CONTACT_NAME_READING,BILL_POSTAL_CODE,BILL_ADDRESS,[BILL_ADDRESS-2],BILL_MAIL_ADDRESS,BILL_PHONE_NUMBER,
                            NCS_CUSTOMER_CODE,[BILL_BANK_ACCOUNT_NAME-1],[BILL_BANK_ACCOUNT_NAME-2],[BILL_BANK_ACCOUNT_NAME-3],[BILL_BANK_ACCOUNT_NAME-4],[BILL_BANK_ACCOUNT_NUMBER-1],
                            [BILL_BANK_ACCOUNT_NUMBER-2],[BILL_BANK_ACCOUNT_NUMBER-3],[BILL_BANK_ACCOUNT_NUMBER-4],BILL_BILLING_INTERVAL,BILL_DEPOSIT_RULES,BILL_TRANSFER_FEE,BILL_EXPENSES,
                            PLAN_SERVER,PLAN_SERVER_LIGHT,PLAN_BROWSER_AUTO,PLAN_BROWSER,PLAN_INITIAL_COST,PLAN_MONTHLY_COST,PLAN_AMIGO_CAI,PLAN_AMIGO_BIZ,PLAN_ADD_BOX_SERVER,
                            PLAN_ADD_BOX_BROWSER,PLAN_OP_FLAT,PLAN_OP_SSL,PLAN_OP_BASIC_SERVICE,PLAN_OP_ADD_SERVICE,PLAN_OP_SOCIOS,PREVIOUS_COMPANY_NAME,NML_CODE_NISSAN,NML_CODE_NS,
                            NML_CODE_JATCO,NML_CODE_AK,NML_CODE_NK,NML_CODE_OTHER,UPDATE_DATE,UPDATER_CODE)VALUES(@COMPANY_NO_BOX,@TRANSACTION_TYPE,@EFFECTIVE_DATE,@UPDATE_CONTENT,@COMPANY_NAME,
                            @COMPANY_NAME_READING,@ESTIMATED_SUBMISSION_DATE,@CONTRACT_DATE,@COMPLETION_NOTE_SENDING_DATE,@CONTRACTOR_DEPARTMENT_IN_CHARGE,@CONTRACTOR_CONTACT_NAME,@CONTRACTOR_CONTACT_NAME_READING,
                            @CONTRACTOR_POSTAL_CODE,@CONTRACTOR_ADDRESS,@CONTRACTOR_ADDRESS_2,@CONTRACTOR_MAIL_ADDRESS,@CONTRACTOR_PHONE_NUMBER,@BILL_SUPPLIER_NAME,@BILL_SUPPLIER_NAME_READING,@BILL_COMPANY_NAME,
                            @BILL_DEPARTMENT_IN_CHARGE,@BILL_CONTACT_NAME,@BILL_CONTACT_NAME_READING,@BILL_POSTAL_CODE,@BILL_ADDRESS,@BILL_ADDRESS_2,@BILL_MAIL_ADDRESS,@BILL_PHONE_NUMBER,@NCS_CUSTOMER_CODE,
                            @BILL_BANK_ACCOUNT_NAME_1,@BILL_BANK_ACCOUNT_NAME_2,@BILL_BANK_ACCOUNT_NAME_3,@BILL_BANK_ACCOUNT_NAME_4,@BILL_BANK_ACCOUNT_NUMBER_1,@BILL_BANK_ACCOUNT_NUMBER_2,@BILL_BANK_ACCOUNT_NUMBER_3,
                            @BILL_BANK_ACCOUNT_NUMBER_4,@BILL_BILLING_INTERVAL,@BILL_DEPOSIT_RULES,@BILL_TRANSFER_FEE,@BILL_EXPENSES,@PLAN_SERVER,@PLAN_SERVER_LIGHT,@PLAN_BROWSER_AUTO,@PLAN_BROWSER,@PLAN_INITIAL_COST,
                            @PLAN_MONTHLY_COST,@PLAN_AMIGO_CAI,@PLAN_AMIGO_BIZ,@PLAN_ADD_BOX_SERVER,@PLAN_ADD_BOX_BROWSER,@PLAN_OP_FLAT,@PLAN_OP_SSL,@PLAN_OP_BASIC_SERVICE,@PLAN_OP_ADD_SERVICE,@PLAN_OP_SOCIOS,
                            @PREVIOUS_COMPANY_NAME,@NML_CODE_NISSAN,@NML_CODE_NS,@NML_CODE_JATCO,@NML_CODE_AK,@NML_CODE_NK,@NML_CODE_OTHER,@UPDATE_DATE,@UPDATER_CODE)";
        string strGetData = "SELECT * FROM CUSTOMER_MASTER";
        string strUpdate = @"UPDATE CUSTOMER_MASTER SET 
                            [BILL_BANK_ACCOUNT_NAME-1] = @BILL_BANK_ACCOUNT_NAME_1,
                            [BILL_BANK_ACCOUNT_NAME-2] = @BILL_BANK_ACCOUNT_NAME_2,
                            [BILL_BANK_ACCOUNT_NAME-3] = @BILL_BANK_ACCOUNT_NAME_3,
                            [BILL_BANK_ACCOUNT_NAME-4] = @BILL_BANK_ACCOUNT_NAME_4,
                            BILL_BILLING_INTERVAL = @BILL_BILLING_INTERVAL,
                            BILL_DEPOSIT_RULES = @BILL_DEPOSIT_RULES,
                            BILL_TRANSFER_FEE = @BILL_TRANSFER_FEE,
                            BILL_EXPENSES = @BILL_EXPENSES,
                            UPDATE_DATE = @UPDATE_DATE,
                            UPDATER_CODE = @UPDATER_CODE,
                            NCS_CUSTOMER_CODE = @NCS_CUSTOMER_CODE
                            WHERE COMPANY_NO_BOX = @COMPANY_NO_BOX AND TRANSACTION_TYPE = @TRANSACTION_TYPE AND FORMAT(EFFECTIVE_DATE, 'yyyyMMdd')=FORMAT(@EFFECTIVE_DATE, 'yyyyMMdd')";
        string strgetGridViewData = @"SELECT COMPANY_NAME, BILL_COMPANY_NAME, COMPANY_NO_BOX,
                                        TRIM([BILL_BANK_ACCOUNT_NAME-1]) [BILL_BANK_ACCOUNT_NAME-1],
                                        TRIM([BILL_BANK_ACCOUNT_NAME-2]) [BILL_BANK_ACCOUNT_NAME-2],
                                        TRIM([BILL_BANK_ACCOUNT_NAME-3]) [BILL_BANK_ACCOUNT_NAME-3],
                                        TRIM([BILL_BANK_ACCOUNT_NAME-4]) [BILL_BANK_ACCOUNT_NAME-4],
                                        NCS_CUSTOMER_CODE, 
                                        (case BILL_BILLING_INTERVAL when 1 then N'月額' when 2 then N'四半期' when 3 then N'半期' when 4 then N'年額' end)BILL_BILLING_INTERVAL,
                                        (case BILL_DEPOSIT_RULES when 0 then N'翌月' when 1 then N'当月' when 2 then N'翌々月月頭' end) BILL_DEPOSIT_RULES, 
                                        BILL_TRANSFER_FEE,BILL_EXPENSES, TRANSACTION_TYPE,  FORMAT(EFFECTIVE_DATE, 'yyyy/MM/dd') EFFECTIVE_DATE
                                        FROM
                                        CUSTOMER_MASTER
                                        WHERE COMPANY_NAME LIKE '%' + @COMPANY_NAME + '%'
                                        AND COMPANY_NAME_READING LIKE '%' + @COMPANY_NAME_READING + '%'
                                        AND BILL_COMPANY_NAME LIKE '%' + @BILL_COMPANY_NAME + '%'
                                        AND COMPANY_NO_BOX LIKE '%' + @COMPANY_NO_BOX + '%'
                                        AND ( 
                                        TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-1])) LIKE '%' + @ACCOUNT_NAME + '%' OR 
                                        TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-2])) LIKE '%' + @ACCOUNT_NAME + '%' OR
                                        TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-3])) LIKE '%' + @ACCOUNT_NAME + '%' OR
                                        TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-4])) LIKE '%' + @ACCOUNT_NAME + '%' )
                                        ORDER BY COMPANY_NO_BOX";
        string strSearchByBankAccountName = @"SELECT * from CUSTOMER_MASTER WHERE
			                                (TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-1])) = @BANK_ACCOUNT_NAME OR
			                                TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-2])) = @BANK_ACCOUNT_NAME OR
			                                TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-3])) = @BANK_ACCOUNT_NAME OR
			                                TRIM(CONVERT(nvarchar(50),CUSTOMER_MASTER.[BILL_BANK_ACCOUNT_NAME-4])) = @BANK_ACCOUNT_NAME) AND
                                            EFFECTIVE_DATE <= GETDATE()
                                            ORDER BY EFFECTIVE_DATE DESC";
       
        #endregion

        #region Constructors

        public CUSTOMER_MASTER(string con)
        {            
            strConnectionString = con;
            strMessage = "";
        }

        #endregion

        #region insert

        public void insert(BOL_CUSTOMER_MASTER B_Customer, out string strMessage)
        {
            strMessage = "";
            ConnectionMaster oMaster = new ConnectionMaster(strConnectionString, strInsert);
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPANY_NO_BOX", B_Customer.COMPANY_NO_BOX));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@TRANSACTION_TYPE", B_Customer.TRANSACTION_TYPE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@EFFECTIVE_DATE", B_Customer.EFFECTIVE_DATE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@UPDATE_CONTENT", B_Customer.UPDATE_CONTENT));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPANY_NAME", B_Customer.COMPANY_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPANY_NAME_READING", B_Customer.COMPANY_NAME_READING));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@ESTIMATED_SUBMISSION_DATE", B_Customer.ESTIMATED_SUBMISSION_DATE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACT_DATE", B_Customer.CONTRACT_DATE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPLETION_NOTE_SENDING_DATE", B_Customer.COMPLETION_NOTE_SENDING_DATE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_DEPARTMENT_IN_CHARGE", B_Customer.CONTRACTOR_DEPARTMENT_IN_CHARGE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_CONTACT_NAME", B_Customer.CONTRACTOR_CONTACT_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_CONTACT_NAME_READING", B_Customer.CONTRACTOR_CONTACT_NAME_READING));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_POSTAL_CODE", B_Customer.CONTRACTOR_POSTAL_CODE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_ADDRESS", B_Customer.CONTRACTOR_ADDRESS));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_ADDRESS_2", B_Customer.CONTRACTOR_ADDRESS_2));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_MAIL_ADDRESS", B_Customer.CONTRACTOR_MAIL_ADDRESS));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@CONTRACTOR_PHONE_NUMBER", B_Customer.CONTRACTOR_PHONE_NUMBER));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_SUPPLIER_NAME", B_Customer.BILL_SUPPLIER_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_SUPPLIER_NAME_READING", B_Customer.BILL_SUPPLIER_NAME_READING));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_COMPANY_NAME", B_Customer.BILL_COMPANY_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_DEPARTMENT_IN_CHARGE", B_Customer.BILL_DEPARTMENT_IN_CHARGE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_CONTACT_NAME", B_Customer.BILL_CONTACT_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_CONTACT_NAME_READING", B_Customer.BILL_CONTACT_NAME_READING));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_POSTAL_CODE", B_Customer.BILL_POSTAL_CODE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_ADDRESS", B_Customer.BILL_ADDRESS));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_ADDRESS_2", B_Customer.BILL_ADDRESS_2));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_MAIL_ADDRESS", B_Customer.BILL_MAIL_ADDRESS));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_PHONE_NUMBER", B_Customer.BILL_PHONE_NUMBER));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NCS_CUSTOMER_CODE", B_Customer.NCS_CUSTOMER_CODE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NAME_1", B_Customer.BILL_BANK_ACCOUNT_NAME_1));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@ILL_BANK_ACCOUNT_NAME_2", B_Customer.BILL_BANK_ACCOUNT_NAME_2));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NAME_3", B_Customer.BILL_BANK_ACCOUNT_NAME_3));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NAME_4", B_Customer.BILL_BANK_ACCOUNT_NAME_4));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NUMBER_1", B_Customer.BILL_BANK_ACCOUNT_NUMBER_1));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NUMBER_2", B_Customer.BILL_BANK_ACCOUNT_NUMBER_2));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NUMBER_3", B_Customer.BILL_BANK_ACCOUNT_NUMBER_3));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NUMBER_4", B_Customer.BILL_BANK_ACCOUNT_NUMBER_4));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BILLING_INTERVAL", B_Customer.BILL_BILLING_INTERVAL));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_DEPOSIT_RULES", B_Customer.BILL_DEPOSIT_RULES));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_TRANSFER_FEE", B_Customer.BILL_TRANSFER_FEE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_EXPENSES", B_Customer.BILL_EXPENSES));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_SERVER", B_Customer.PLAN_SERVER));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_SERVER_LIGHT", B_Customer.PLAN_SERVER_LIGHT));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_BROWSER_AUTO", B_Customer.PLAN_BROWSER_AUTO));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_BROWSER", B_Customer.PLAN_BROWSER));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_INITIAL_COST", B_Customer.PLAN_INITIAL_COST));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_MONTHLY_COST", B_Customer.PLAN_MONTHLY_COST));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_AMIGO_CAI", B_Customer.PLAN_AMIGO_CAI));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_AMIGO_BIZ", B_Customer.PLAN_AMIGO_BIZ));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_ADD_BOX_SERVER", B_Customer.PLAN_ADD_BOX_SERVER));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_ADD_BOX_BROWSER", B_Customer.PLAN_ADD_BOX_BROWSER));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_OP_FLAT", B_Customer.PLAN_OP_FLAT));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_OP_SSL", B_Customer.PLAN_OP_SSL));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_OP_BASIC_SERVICE", B_Customer.PLAN_OP_BASIC_SERVICE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_OP_ADD_SERVICE", B_Customer.PLAN_OP_ADD_SERVICE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PLAN_OP_SOCIOS", B_Customer.PLAN_OP_SOCIOS));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@PREVIOUS_COMPANY_NAME", B_Customer.PREVIOUS_COMPANY_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NML_CODE_NISSAN", B_Customer.NML_CODE_NISSAN));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NML_CODE_NS", B_Customer.NML_CODE_NS));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NML_CODE_JATCO", B_Customer.NML_CODE_JATCO));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NML_CODE_AK", B_Customer.NML_CODE_AK));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NML_CODE_NK", B_Customer.NML_CODE_NK));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NML_CODE_OTHER", B_Customer.NML_CODE_OTHER));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@UPDATE_DATE", B_Customer.UPDATE_DATE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@UPDATER_CODE", B_Customer.UPDATER_CODE));
            oMaster.ExcuteQuery(1, out strMessage);
            //_M01 = oMaster.intRtnID;
            //  return intRtn;
        }

        #endregion

        #region getData
        public DataTable getDataByAll()
        {
            ConnectionMaster oMaster = new ConnectionMaster(strConnectionString, strGetData);
            oMaster.ExcuteQuery(4, out strMessage);
            return oMaster.dtExcuted;
        }
        #endregion

        #region getGridViewData
        public DataTable getGridViewData(string COMPANY_NAME, string COMPANY_NAME_READING, string BILL_COMPANY_NAME, string COMPANY_NO_BOX, string ACCOUNT_NAME)
        {
            ConnectionMaster oMaster = new ConnectionMaster(strConnectionString, strgetGridViewData);
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPANY_NAME", COMPANY_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPANY_NAME_READING", COMPANY_NAME_READING));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_COMPANY_NAME", BILL_COMPANY_NAME));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPANY_NO_BOX", COMPANY_NO_BOX));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@ACCOUNT_NAME", ACCOUNT_NAME));
            oMaster.ExcuteQuery(4, out strMessage);
            return oMaster.dtExcuted;
        }
        #endregion

        #region getGridViewData
        public DataTable SearchByBankAccountName(string BANK_ACCOUNT_NAME)
        {
            ConnectionMaster oMaster = new ConnectionMaster(strConnectionString, strSearchByBankAccountName);
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BANK_ACCOUNT_NAME", BANK_ACCOUNT_NAME));
            oMaster.ExcuteQuery(4, out strMessage);
            return oMaster.dtExcuted;
        }
        #endregion

        #region update
        public void update(BOL_CUSTOMER_MASTER B_Customer, out string strMessage)
        {
            strMessage = "";
            ConnectionMaster oMaster = new ConnectionMaster(strConnectionString, strUpdate);
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NAME_1", B_Customer.BILL_BANK_ACCOUNT_NAME_1));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NAME_2", B_Customer.BILL_BANK_ACCOUNT_NAME_2));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NAME_3", B_Customer.BILL_BANK_ACCOUNT_NAME_3));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BANK_ACCOUNT_NAME_4", B_Customer.BILL_BANK_ACCOUNT_NAME_4));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_BILLING_INTERVAL", B_Customer.BILL_BILLING_INTERVAL));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_DEPOSIT_RULES", B_Customer.BILL_DEPOSIT_RULES));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_TRANSFER_FEE", B_Customer.BILL_TRANSFER_FEE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@BILL_EXPENSES", B_Customer.BILL_EXPENSES));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@UPDATE_DATE", B_Customer.UPDATE_DATE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@UPDATER_CODE", B_Customer.UPDATER_CODE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@COMPANY_NO_BOX", B_Customer.COMPANY_NO_BOX));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@TRANSACTION_TYPE", B_Customer.TRANSACTION_TYPE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@NCS_CUSTOMER_CODE", B_Customer.NCS_CUSTOMER_CODE));
            oMaster.crudCommand.Parameters.Add(new SqlParameter("@EFFECTIVE_DATE", B_Customer.EFFECTIVE_DATE));
            oMaster.ExcuteQuery(2, out strMessage);
        }

        #endregion

    }
    #endregion

}
