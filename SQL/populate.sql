USE [LibrarySystemDb]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookId], [Title], [Publisher], [Isbn], [Summary], [Language], [Year], [Edition], [CreatedOn], [ModifiedOn]) VALUES (1, N'Johny', N'Bravo', N'isbn-13333', N'samo johni', N'English', 2000, 1, CAST(N'2021-05-24T03:33:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Books] ([BookId], [Title], [Publisher], [Isbn], [Summary], [Language], [Year], [Edition], [CreatedOn], [ModifiedOn]) VALUES (2, N'Hello', N'Kitty', N'isbn-25232', N'detsko', N'Deutsch', 1822, 1, CAST(N'2021-05-24T03:33:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Books] ([BookId], [Title], [Publisher], [Isbn], [Summary], [Language], [Year], [Edition], [CreatedOn], [ModifiedOn]) VALUES (3, N'Bravo Johny', N'Kaito', N'isbbn-52323', N'samo', N'Dutch', 1992, 1, CAST(N'2021-05-23T07:33:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Books] ([BookId], [Title], [Publisher], [Isbn], [Summary], [Language], [Year], [Edition], [CreatedOn], [ModifiedOn]) VALUES (4, N'John Carter Between worlds', N'Victor Gyoshev', N'ISBN', N'Summm', N'Bulgarian', 2020, 1, CAST(N'2021-05-21T03:33:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [DisplayName], [Password], [Settings_SettingsId]) VALUES (1, N'123', N'123', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 22)
INSERT [dbo].[Users] ([UserId], [Username], [DisplayName], [Password], [Settings_SettingsId]) VALUES (2, N'Sasho', N'Sasho', N'e3fca228564d6370fcd616ffb7a17a7f37e474d810a0b5e3a5bfa42b6ef28315', NULL)
INSERT [dbo].[Users] ([UserId], [Username], [DisplayName], [Password], [Settings_SettingsId]) VALUES (3, N'Victor', N'Victor', N'9f2466eb60109a480203a5a453a8a0c38fd477ac494654ff8fa8401764c21989', 4)
INSERT [dbo].[Users] ([UserId], [Username], [DisplayName], [Password], [Settings_SettingsId]) VALUES (4, N'Petko', N'Petko', N'08b994059d95e559074cb6c74991d9373160781637a24fe1903ee1d4b979d23e', 7)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UserBooks] ([User_UserId], [Book_BookId]) VALUES (1, 1)
INSERT [dbo].[UserBooks] ([User_UserId], [Book_BookId]) VALUES (2, 1)
INSERT [dbo].[UserBooks] ([User_UserId], [Book_BookId]) VALUES (1, 2)
INSERT [dbo].[UserBooks] ([User_UserId], [Book_BookId]) VALUES (2, 2)
INSERT [dbo].[UserBooks] ([User_UserId], [Book_BookId]) VALUES (1, 3)
INSERT [dbo].[UserBooks] ([User_UserId], [Book_BookId]) VALUES (1, 4)
GO
SET IDENTITY_INSERT [dbo].[Passwords] ON 

INSERT [dbo].[Passwords] ([PasswordId], [PasswordString], [User_UserId]) VALUES (1, N'tuEPz8Lpcxw9qtc32rkYAQ==', 1)
INSERT [dbo].[Passwords] ([PasswordId], [PasswordString], [User_UserId]) VALUES (2, N'lb7ZmXdBQS6NJNIvP7KJ3A==', 1)
INSERT [dbo].[Passwords] ([PasswordId], [PasswordString], [User_UserId]) VALUES (3, N'M8VSZLzDJPdrYL5IHwW2Lw==', 1)
SET IDENTITY_INSERT [dbo].[Passwords] OFF
GO
SET IDENTITY_INSERT [dbo].[Settings] ON 

INSERT [dbo].[Settings] ([SettingsId], [SuggestionsCount], [SuggestionTimeInterval], [SuggestionTimeIntervalString], [AutoComplete], [InputLengthThreshold]) VALUES (4, 4, 315360000000000, N'Последната година', 0, 0)
INSERT [dbo].[Settings] ([SettingsId], [SuggestionsCount], [SuggestionTimeInterval], [SuggestionTimeIntervalString], [AutoComplete], [InputLengthThreshold]) VALUES (7, 0, 0, NULL, 0, 0)
INSERT [dbo].[Settings] ([SettingsId], [SuggestionsCount], [SuggestionTimeInterval], [SuggestionTimeIntervalString], [AutoComplete], [InputLengthThreshold]) VALUES (22, 5, 0, N'Без', 0, 3)
SET IDENTITY_INSERT [dbo].[Settings] OFF
GO
