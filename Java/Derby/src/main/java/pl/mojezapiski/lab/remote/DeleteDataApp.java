package pl.mojezapiski.lab.remote;

import java.sql.*;

public class DeleteDataApp {

    public DeleteDataApp() {
    }

    public void deleteData() {
        Connection con = null;
        try {
            // Ładujemy plik z klasą sterownika bazy danych
            Class.forName("org.apache.derby.jdbc.ClientDriver");
            // Tworzymy połączenie do bazy danych
            con = DriverManager.getConnection("jdbc:derby://localhost:1527/lab", "lab", "lab");
            // Tworzymy obiekt wyrażenia
            ///Statement statement = con.createStatement();
            Statement statement = con.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
            // Usuwamy dane z tabeli
            ///statement.executeUpdate("DELETE FROM Dane WHERE nazwisko = 'Mickiewicz'");
            //////////////////Moje
            ResultSet rs = statement.executeQuery("SELECT * FROM Dane WHERE nazwisko = 'YYYYY'");
            rs.first();
            rs.deleteRow();
            
            
            
            System.out.println("Data removed");
        } catch (SQLException sqle) {
            System.err.println("SQL exception: " + sqle.getMessage());
        } catch (ClassNotFoundException cnfe) {
            System.err.println("ClassNotFound exception: " + cnfe.getMessage());
        } catch (Exception e) {
            System.err.println("Another exception: " + e.getMessage());
        } finally {
            try {
                if (con != null) {
                    con.close();
                }
            } catch (SQLException sqle) {
                System.err.println("SQL exception: " + sqle.getMessage());
            }
        }
    }

    public static void main(String[] args) {
        DeleteDataApp deleteDataApp = new DeleteDataApp();
        deleteDataApp.deleteData();
    }
}
