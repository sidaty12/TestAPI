
ExmpleApi: Une API ASP.NET Core sécurisée avec JWT, Identity, versioning et documentation
ExmpleApi est un exemple d'API sécurisée utilisant ASP.NET Core Identity pour l'authentification et l'autorisation des utilisateurs avec des tokens JWT. Elle supporte également le versionnage des API et fournit une documentation automatisée via Swagger.

Caractéristiques
Inscription et connexion des utilisateurs.
Gestion des rôles pour les utilisateurs.
Protection des endpoints d'API avec authentification et autorisation basées sur les rôles.
Token JWT pour une authentification stateless.
Versionnage de l'API pour une meilleure gestion des changements.
Documentation de l'API automatisée grâce à Swagger.
Besoin traité
ExmpleApi a été conçue pour traiter le besoin de sécuriser une API ASP.NET Core et de gérer les utilisateurs et leurs rôles de manière efficace. Cela permet de protéger les ressources de l'API et de s'assurer que seuls les utilisateurs autorisés peuvent accéder aux différentes fonctionnalités.

Fonctionnement de l'API
L'API possède plusieurs contrôleurs pour gérer les différentes fonctionnalités:

AccountController: Gère l'inscription des utilisateurs.

POST /api/account/register: Permet à un nouvel utilisateur de s'inscrire.
AuthController: Gère la connexion des utilisateurs et la délivrance des tokens JWT.

POST /api/auth/login: Authentifie un utilisateur et renvoie un token JWT si les informations d'identification sont valides.
AdminController: Gère l'assignation des rôles aux utilisateurs. Ce contrôleur est protégé et requiert que l'utilisateur authentifié possède le rôle "Admin".

POST /api/admin/assignrole: Permet d'assigner un rôle à un utilisateur spécifique.
Lors de l'authentification, un token JWT est retourné. Ce token doit être inclus dans le header Authorization des requêtes suivantes pour accéder aux endpoints protégés.

Documentation et versionnage
La documentation de l'API est automatisée grâce à Swagger. Vous pouvez accéder à la documentation en naviguant vers /swagger sur votre instance locale de l'API.

Le versionnage de l'API est supporté et permet de gérer facilement les modifications de l'API sans briser les applications clientes existantes. Vous pouvez spécifier la version de l'API dans l'URL de la requête.

Problèmes rencontrés et solutions
Pendant le développement de l'API, nous avons rencontré plusieurs problèmes. Le plus courant étant l'erreur 403 Forbidden lorsque l'on essayait d'accéder à un endpoint protégé avec un utilisateur n'ayant pas les droits nécessaires. Pour résoudre ce problème, nous avons dû vérifier que l'utilisateur authentifié possède le bon rôle pour accéder à la ressource.

Une autre difficulté rencontrée était l'assignation des rôles aux utilisateurs. Si un rôle spécifique n'existait pas dans la base de données, une erreur 400 Bad Request était retournée. Pour résoudre ce problème, il fallait s'assurer que les rôles étaient correctement créés lors de l'initialisation de l'application.

Conclusion
ExmpleApi est un excellent exemple de la façon dont on peut sécuriser une API ASP.NET Core avec Identity et JWT, tout en supportant le versionnage et la documentation automatisée. Le projet démontre comment on peut gérer les utilisateurs et leurs rôles, et comment on peut utiliser ces rôles pour protéger l'accès à certaines ressources de l'API.
