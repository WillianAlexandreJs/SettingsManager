﻿
CREATE PROCEDURE [dbo].[GET_INSTANCE_PROPERTY_VALUES] (
@INSTANCE_NAME VARCHAR(200)
)
AS
BEGIN
	SELECT INST.INSTANCE_NAME,
		   PROP.PROPERTY_NAME,
		   PROP.PROPERTY_TYPE,
		   PROP.SETTING_REFERENCE,
		   INSTPROP.PROPERTY_VALUE
	FROM TB_APPLICATION APP
	INNER JOIN TB_PROPERTY PROP ON APP.APPLICATION_ID = PROP.APPLICATION_ID
	INNER JOIN TB_INSTANCE INST ON APP.APPLICATION_ID = INST.APPLICATION_ID 
	INNER JOIN TB_INSTANCE_PROPERTY INSTPROP ON INST.INSTANCE_ID = INSTPROP.INSTANCE_PROPERTY_ID
											AND PROP.PROPERTY_ID = INSTPROP.PROPERTY_ID
	WHERE INST.ST_ENABLE = 1
	AND INST.INSTANCE_NAME = @INSTANCE_NAME
	UNION
	SELECT CLI_INST.INSTANCE_NAME,
		   CLI_PROP.PROPERTY_NAME,
		   CLI_PROP.PROPERTY_TYPE,
		   CLI_PROP.SETTING_REFERENCE,
		   SRV_INSTPROP.PROPERTY_VALUE
	FROM TB_INSTANCE CLI_INST 
	INNER JOIN TB_APPLICATION CLI_APP ON CLI_APP.APPLICATION_ID = CLI_INST.APPLICATION_ID 
	INNER JOIN TB_PROPERTY CLI_PROP ON CLI_APP.APPLICATION_ID = CLI_PROP.APPLICATION_ID
	INNER JOIN TB_INSTANCE_INTEGRATION INSTINT ON CLI_INST.INSTANCE_ID = INSTINT.CLIENT_INSTANCE_ID
											   AND CLI_PROP.PROPERTY_ID = INSTINT.CLIENT_PROPERTY_ID
	INNER JOIN TB_INSTANCE SRV_INST ON INSTINT.SERVER_INSTANCE_ID = SRV_INST.INSTANCE_ID 
	INNER JOIN TB_APPLICATION SRV_APP ON SRV_APP.APPLICATION_ID = SRV_INST.APPLICATION_ID 
	INNER JOIN TB_PROPERTY SRV_PROP ON SRV_APP.APPLICATION_ID = SRV_PROP.APPLICATION_ID
									AND INSTINT.SERVER_PROPERTY_ID = SRV_PROP.PROPERTY_ID
	INNER JOIN TB_INSTANCE_PROPERTY SRV_INSTPROP ON SRV_INST.INSTANCE_ID = SRV_INSTPROP.INSTANCE_ID
											AND SRV_PROP.PROPERTY_ID = SRV_INSTPROP.PROPERTY_ID
	WHERE INSTINT.ST_ENABLE = 1
	AND CLI_INST.INSTANCE_NAME = @INSTANCE_NAME
	UNION
	SELECT CLI_INST.INSTANCE_NAME,
		   LIBPROP.LIBRARY_PROPERTY_NAME AS PROPERTY_NAME,
		   LIBPROP.LIBRARY_PROPERTY_TYPE AS PROPERTY_TYPE,
		   LIB.LIBRARY_NAME AS SETTING_REFERENCE,
		   INSTREF.PROPERTY_VALUE
	FROM TB_INSTANCE CLI_INST 
	INNER JOIN TB_APPLICATION CLI_APP ON CLI_APP.APPLICATION_ID = CLI_INST.APPLICATION_ID 
	INNER JOIN TB_INSTANCE_LABRARY_REFERENCE INSTREF ON CLI_INST.INSTANCE_ID = INSTREF.INSTANCE_ID
	INNER JOIN TB_LIBRARY_PROPERTY LIBPROP ON INSTREF.LIBRARY_PROPERTY_ID = LIBPROP.LIBRARY_PROPERTY_ID
	INNER JOIN TB_LIBRARY LIB ON LIBPROP.LIBRARY_ID = LIB.LIBRARY_ID
	WHERE INSTREF.ST_ENABLE = 1
	AND CLI_INST.INSTANCE_NAME = @INSTANCE_NAME
END