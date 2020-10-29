﻿CREATE TABLE [dbo].[TB_INSTANCE_INTEGRATION] (
    [INSTANCE_INTEGRATION_ID] INT          IDENTITY (1, 1) NOT NULL,
    [CLIENT_INSTANCE_ID]      INT          NOT NULL,
    [CLIENT_PROPERTY_ID]      INT          NOT NULL,
    [SERVER_INSTANCE_ID]      INT          NULL,
    [SERVER_PROPERTY_ID]      INT          NULL,
    [USER_CREATED]            VARCHAR (40) NOT NULL,
    [DT_CREATED]              DATETIME     CONSTRAINT [DT_INSTANCE_INTEGRATION_CREATED] DEFAULT (getdate()) NOT NULL,
    [USER_UPDATED]            VARCHAR (40) NULL,
    [DT_UPDATED]              DATETIME     NULL,
    [ST_ENABLE]               BIT          CONSTRAINT [DT_INSTANCE_INTEGRATION_ENABLE] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_INSTANCE_INTEGRATION] PRIMARY KEY CLUSTERED ([INSTANCE_INTEGRATION_ID] ASC),
    CONSTRAINT [FK_CLIENT_INSTANCE_INTEGRATION_INSTANCE] FOREIGN KEY ([CLIENT_INSTANCE_ID]) REFERENCES [dbo].[TB_INSTANCE] ([INSTANCE_ID]),
    CONSTRAINT [FK_CLIENT_INSTANCE_INTEGRATION_PROPERTY] FOREIGN KEY ([CLIENT_PROPERTY_ID]) REFERENCES [dbo].[TB_PROPERTY] ([PROPERTY_ID]),
    CONSTRAINT [FK_SERVER_INSTANCE_INTEGRATION_INSTANCE] FOREIGN KEY ([SERVER_INSTANCE_ID]) REFERENCES [dbo].[TB_INSTANCE] ([INSTANCE_ID]),
    CONSTRAINT [FK_SERVER_INSTANCE_INTEGRATION_PROPERTY] FOREIGN KEY ([SERVER_PROPERTY_ID]) REFERENCES [dbo].[TB_PROPERTY] ([PROPERTY_ID])
);

