# AllLyrics
Simple lyrics website with search by artists and song titles.
Site administration area: /Admin, default admin credentials: login - "admin", password - "qwerty".
Connection string for the database should be set in appsettings.json, "ConnectionStrings" section.
About authentication: CustomCookieAuthenticationEvents guarantees that user will lose access to the admin area immediately after his administrator account is deleted.
