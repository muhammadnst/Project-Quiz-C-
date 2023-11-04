CREATE TABLE [dbo].[QUESTION] (
    [QUESTION_ID] INT            IDENTITY (1, 1) NOT NULL,
    [Q_TEXT]      NVARCHAR (MAX) NOT NULL,
    [OPA]         NVARCHAR (100) NOT NULL,
    [OPB]         NVARCHAR (100) NOT NULL,
    [OPC]         NVARCHAR (100) NOT NULL,
    [OPD]         NVARCHAR (100) NOT NULL,
    [COP]         NVARCHAR (100) NOT NULL,
    [q_fk_catid]  INT            NULL,
    PRIMARY KEY CLUSTERED ([QUESTION_ID] ASC),
    UNIQUE NONCLUSTERED ([OPA] ASC),
    UNIQUE NONCLUSTERED ([OPB] ASC),
    UNIQUE NONCLUSTERED ([OPC] ASC),
    UNIQUE NONCLUSTERED ([OPD] ASC),
    FOREIGN KEY ([q_fk_catid]) REFERENCES [dbo].[tbl_category] ([cat_id])
);

