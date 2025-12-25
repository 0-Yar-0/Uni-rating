# Смена пароля

Эндпоинт для изменения пароля пользователя (требуется аутентификация JWT).

- URL: `POST /api/auth/change-password`
- Заголовки: `Authorization: Bearer <JWT_TOKEN>`
- Тело (JSON):

```json
{
  "currentPassword": "old_password",
  "newPassword": "new_secure_password"
}
```

Пример curl:

```bash
curl -X POST https://localhost:5001/api/auth/change-password \
  -H "Authorization: Bearer <TOKEN>" \
  -H "Content-Type: application/json" \
  -d '{"currentPassword":"old","newPassword":"newPass123"}'
```

Замечания:
- Новый пароль должен быть минимум 6 символов (проверка на сервере).
- Токен генерируется при логине (`POST /api/auth/login`). Ответ от login теперь содержит реальный JWT в поле `token`.
