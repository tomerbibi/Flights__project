﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Flights__Project
{
    class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if(token.User.Id == flight.AirlineCompanyID)
                _flightDAOPGSQL.Remove(flight);
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            User u = new User();
            _userDAOPGSQL.GetAll().ForEach(_ =>
            {
                if (_.Id == token.User.UserId)
                    u = _;
            });

            if (u.Password == oldPassword)
            {
                u.Password = newPassword;
                _userDAOPGSQL.Update(u);
            }
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token.User.Id == flight.AirlineCompanyID)
                _flightDAOPGSQL.Add(flight);
        }

        public List<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            List<Flight> f = new List<Flight>();
            _flightDAOPGSQL.GetAll().ForEach(flight =>
            {
                if (flight.AirlineCompanyID == token.User.Id)
                {
                    f.Add(flight);
                }
            });
            return f;
        }

        public List<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            List<Ticket> t = new List<Ticket>();
            _flightDAOPGSQL.GetAll().ForEach(flight =>
            {
                if (flight.AirlineCompanyID == token.User.Id)
                {
                    _ticketDAOPGSQL.GetAll().ForEach(ticket =>
                    {
                        if(ticket.FlightID == flight.Id)
                        {
                            t.Add(ticket);
                        }
                    });
                }
            });
            return t;
        }

        public void MofidyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if(token.User.Id == airline.Id)
                _airlineDAOPGSQL.Update(airline);
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token.User.Id == flight.AirlineCompanyID)
                _flightDAOPGSQL.Update(flight);
        }
    }
}