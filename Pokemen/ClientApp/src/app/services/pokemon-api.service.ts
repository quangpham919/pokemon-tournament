import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { PokemonStat } from '../../Models/PokemonStat';

@Injectable({
  providedIn: 'root'
})
export class PokemonAPIService {
  constructor(private http: HttpClient) { }

  getTournamentStatistics(sortBy: string, sortDirection?: string) {
    let params: {
      sortBy: string,
      sortDirection?: string
    } = {
      sortBy
    };

    if (sortDirection) {
      params.sortDirection = sortDirection
    }

    return this.http.get<PokemonStat[]>("pokemon/tournament/statistics", {
      params: params
    });
  }

}
