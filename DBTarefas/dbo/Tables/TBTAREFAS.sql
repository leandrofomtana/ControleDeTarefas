CREATE TABLE [dbo].[TBTAREFAS] (
    [ID]                  INT           IDENTITY (1, 1) NOT NULL,
    [TITULO]              VARCHAR (200) NULL,
    [DATACRIACAO]         DATETIME      NULL,
    [DATACONCLUSAO]       DATETIME      NULL,
    [PERCENTUALCONCLUIDO] DECIMAL (18)  NULL,
    [PRIORIDADE]          INT           NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

