﻿using WaveCenter.Model;

namespace WaveCenter.ModelsAPI
{
    public class InsertExperiencia
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public int IdLocal { get; set; }
        public string Imagem { get; set; } = string.Empty;
        public int NumeroMinimoPessoas { get; set; }
        public int NumeroMaximoPessoas { get; set; }
        // Em horas e.g 1.5 -> hora e meia 
        public double DuracaoMinima { get; set; }
        public double DuracaoMaxima { get; set; }
        public double HoraComecoDia { get; set; }
        public double HoraFimDia { get; set; }
        public int IdTipoExperiencia { get; set; }
        public double PrecoHora { get; set; }
        public int IdCategoriaExperiencia { get; set; }
        public bool Ativo { get; set; }
    }
}