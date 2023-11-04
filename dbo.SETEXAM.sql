CREATE TABLE [dbo].[SETEXAM] (
    [EXAM_ID]       INT           IDENTITY (1, 1) NOT NULL,
    [EXAM_DATE]     DATETIME      NULL,
    [EXAM_FK_STU]   INT           NULL,
    [EXAM_NAME]     NVARCHAR (50) NOT NULL,
    [STD_STD_SCORE] INT           NULL,
    PRIMARY KEY CLUSTERED ([EXAM_ID] ASC),
    FOREIGN KEY ([EXAM_FK_STU]) REFERENCES [dbo].[STUDENT] ([S_ID])
);

