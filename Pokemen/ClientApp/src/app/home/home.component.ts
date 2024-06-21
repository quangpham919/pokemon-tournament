import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DropdownOption } from '../../Models/DropDownOption';
import { PokemonStat } from '../../Models/PokemonStat';
import { SortCol } from '../../Models/SortCol';
import { PokemonAPIService } from '../services/pokemon-api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ["./home.component.css"]
})
export class HomeComponent {

  public pokemons: PokemonStat[] = []
  public hasPokemons: number = 0;
  public loading: boolean = false;
  public headers: string[] = ["Id", "Name", "Type", "Wins", "Loses", "Ties"]
  public sortBy: string = "";
  public errorMessage: string = "";

  public sortByOptions: DropdownOption[] = [
    {
      label: "name",
      value: "Name"
    },
    {
      label: "id",
      value: "Id"
    },
    {
      label: "wins",
      value: "Wins"
    },
    {
      label: "loses",
      value: "Loses"
    },
    {
      label: "ties",
      value: "Ties"
    }
  ];

  public sortDirectionOptions: DropdownOption[] = [
    {
      label: "Asc",
      value: "ascending"
    },
    {
      label: "Desc",
      value: "descending"
    }
  ];


  public formGroup = this.fb.group({
    sortBy: ["", Validators.required],
    sortDirection: [""]
  });

  constructor(private pokemonApiService: PokemonAPIService, private fb: FormBuilder) {
  }

  ngOnInit() {
  }

  fetchTournamentStats(sortBy: string, sortDirection?: string) {
    this.errorMessage = "";
    this.loading = true;
    this.pokemonApiService.getTournamentStatistics(
      sortBy,
      sortDirection)
      .subscribe(
        (result) => {
          this.pokemons = result;
          this.hasPokemons = result.length;
          this.loading = false;
        },
        (error) => {
          this.errorMessage = error.error;
          this.loading = false;
        }
    )
  }

  submitHandler() {
    console.log(this.formGroup.controls);
    this.fetchTournamentStats(
      this.formGroup.controls.sortBy.value ?? "",
      this.formGroup.controls.sortDirection.value ?? "");    
  }
}

