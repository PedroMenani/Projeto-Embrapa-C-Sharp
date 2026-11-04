CREATE DATABASE IF NOT EXISTS projeto_integrador_db CHARACTER SET utf8mb4;

USE projeto_integrador_db;

-- CREATE DATABASE IF NOT EXISTS cadastro_db CHARACTER SET utf8mb4;

-- USE cadastro_db;

-- CREATE TABLE IF NOT EXISTS usuarios(
--     id    INT AUTO_INCREMENT PRIMARY KEY,
--     nome  VARCHAR(100) NOT NULL,
--     email VARCHAR(100) NOT NULL UNIQUE,
--     senha VARCHAR (64) NOT NULL
-- );

-- INSERT IGNORE INTO usuarios(nome,email,senha)
-- VALUES ('Administrador','admin@admin.com','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918');

CREATE TABLE cargo(
    id_cargo INT PRIMARY KEY,
    cargo    VARCHAR(50) NOT NULL

);

CREATE TABLE usuario(
    matricula INT PRIMARY KEY,
    nome      VARCHAR(50) NOT NULL,
    email     VARCHAR(50) NOT NULL,
    cargo     INT NOT NULL,
    FOREIGN KEY (cargo) REFERENCES cargo(cargo)
);

CREATE TABLE acao(
    id_acao INT PRIMARY KEY,
    titulo_acao VARCHAR(50) NOT NULL,
    data_realizacao DATE,
    matricula_pesquisador INT NOT NULL,
    status  VARCHAR(50) NOT NULL,

    FOREIGN KEY (matricula_pesquisador) REFERENCES usuario(matricula)
);

CREATE TABLE log(
    id_log INT PRIMARY KEY,
    operacao VARCHAR(50) NOT NULL,
    tabela_afetada VARCHAR(50) NOT NULL,
    data_hora DATETIME,
    matricula INT
);

