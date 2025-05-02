USE [GestorDeTarefasDb]
GO

/****** Object:  Table [dbo].[Tarefas]    Script Date: 01/05/2025 22:23:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tarefas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](255) NOT NULL,
	[Descricao] [varchar](500) NOT NULL,
	[Status] [int] NOT NULL,
	[DataVencimento] [datetime] NOT NULL
) ON [PRIMARY]
GO


