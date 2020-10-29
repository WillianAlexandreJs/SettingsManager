﻿CREATE TABLE [dbo].[TB_INSTANCE] (
    [INSTANCE_ID]    INT          IDENTITY (1, 1) NOT NULL,
    [INSTANCE_NAME]  VARCHAR (50) NOT NULL,
    [APPLICATION_ID] INT          NOT NULL,
    [USER_CREATED]   VARCHAR (40) NOT NULL,
    [DT_CREATED]     DATETIME     CONSTRAINT [DT_INSTANCE_CREATED] DEFAULT (getdate()) NOT NULL,
    [USER_UPDATED]   VARCHAR (40) NULL,
    [DT_UPDATED]     DATETIME     NULL,
    [ST_ENABLE]      BIT          CONSTRAINT [DT_INSTANCE_ENABLE] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_INSTANCE] PRIMARY KEY CLUSTERED ([INSTANCE_ID] ASC),
    CONSTRAINT [FK_APPLICATION_INSTANCE] FOREIGN KEY ([APPLICATION_ID]) REFERENCES [dbo].[TB_APPLICATION] ([APPLICATION_ID]),
    CONSTRAINT [UN_INSTANCE_NAME] UNIQUE NONCLUSTERED ([INSTANCE_NAME] ASC)
);
