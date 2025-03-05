# Variaveis
DEV_COMPOSE=docker-compose.dev.yaml
PROD_COMPOSE=docker-compose.prod.yaml
SERVICE_NAME=FlexPro-Api

dev:
	docker-compose -f $(DEV_COMPOSE) up --build -d

dev-stop:
	docker-compose -f $(DEV_COMPOSE) down

dev-status:
	docker-compose -f $(DEV_COMPOSE) ps

prod:
	docker-compose -f $(PROD_COMPOSE) up --build -d

prod-stop:
	docker-compose -f $(PROD_COMPOSE) down

prod-status:
	docker-compose -f $(PROD_COMPOSE) ps

dev-update:
	docker-compose -f $(DEV_COMPOSE) up --build -d

prod-update:
	docker-compose -f $(PROD_COMPOSE) up --build -d

dev-remove:
	docker-compose -f $(DEV_COMPOSE) down --volumes

prod-remove:
	docker-compose -f $(PROD_COMPOSE) down --volumes
