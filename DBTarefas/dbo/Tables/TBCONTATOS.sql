﻿CREATE TABLE [dbo].[TBCONTATOS] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [NOME]     VARCHAR (200) NULL,
    [EMAIL]    VARCHAR (200) NULL,
    [TELEFONE] VARCHAR (200) NULL,
    [EMPRESA]  VARCHAR (200) NULL,
    [CARGO]    VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
