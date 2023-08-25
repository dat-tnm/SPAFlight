import { Component, OnInit } from '@angular/core';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models'

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit{
  searchResults: any = [];


  constructor(private flightService: FlightService) { }

  ngOnInit(): void {
  }

  search() {
    this.flightService.searchFlight({})
      .subscribe(response => this.searchResults = response,
      this.handleError);
  }

  private handleError(error: any) {
    console.log(error);
  }
}
