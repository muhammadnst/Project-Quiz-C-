CREATE TABLE [dbo].[tbl_category] (
    [cat_id]              INT            IDENTITY (1, 1) NOT NULL,
    [cat_name]            NVARCHAR (50)  NOT NULL,
    [cat_fk_adid]         INT            NULL,
    [cat_encryptedstring] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([cat_id] ASC),
    FOREIGN KEY ([cat_fk_adid]) REFERENCES [dbo].[ADMINS] ([AD_ID])
);

